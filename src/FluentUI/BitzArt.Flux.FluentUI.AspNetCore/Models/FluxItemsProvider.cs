using BitzArt.Pagination;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace BitzArt.Flux;

public class FluxItemsProvider<TModel>
    where TModel : class
{
    private readonly IFluxSetContext<TModel> _fluxSet;
    protected readonly PaginationState PaginationState;

    public GridItemsProvider<TModel> GetItems => new(GetItemsAsync);
    protected FluxSortMap<TModel> SortMap { get; }

    private FluxPageRequestRecord<TModel>? _lastRequest = null;

    public delegate void OnAfterRequestHandler(FluxPageRequestRecord<TModel> request);
    public event OnAfterRequestHandler? OnAfterRequest;

    public FluxItemsProvider(IFluxSetContext<TModel> fluxSet, PaginationState paginationState)
    {
        SortMap = new();
        _fluxSet = fluxSet;
        PaginationState = paginationState;
    }

    private async ValueTask<GridItemsProviderResult<TModel>> GetItemsAsync(GridItemsProviderRequest<TModel> request)
    {
        var pageRequest = new PageRequest(request.StartIndex, request.Count);

        pageRequest = await ConfigurePageRequestAsync(pageRequest);
        var sort = GetSorting(request);
        if (_lastRequest is not null
            && _lastRequest.Request.Sorting.Compare(sort) == false
            && (!_lastRequest.IsExhausted.HasValue || _lastRequest.IsExhausted.Value == false))
        {
            _lastRequest.IsExhausted = true;
            await PaginationState.SetCurrentPageIndexAsync(0);
            return GridItemsProviderResult.From(
                items: _lastRequest.Result!.Data!.ToList(),
                totalItemCount: _lastRequest.Result.Total!.Value);
        }

        var parameters = await ConfigureParametersAsync(sort, request);

        var currentRequest = new FluxPageRequestRecord<TModel>(new(pageRequest, sort, parameters));
        if (_lastRequest is not null
            && _lastRequest.Request.Compare(currentRequest.Request) == true
            && _lastRequest.Result is not null)
            return FinalizeResult(currentRequest, _lastRequest.Result!);

        var page = await _fluxSet!.GetPageAsync(
            pageRequest,
            //request.CancellationToken,
            parameters: parameters);

        return FinalizeResult(currentRequest, page);
    }

    protected virtual Task<PageRequest> ConfigurePageRequestAsync(PageRequest pageRequest) => Task.FromResult(pageRequest);

    protected virtual Task<object[]> ConfigureParametersAsync(FluxSortingInfo sort, GridItemsProviderRequest<TModel> request)
        => Task.FromResult(Array.Empty<object>());

    private GridItemsProviderResult<TModel> FinalizeResult(FluxPageRequestRecord<TModel> request, PageResult<TModel> page)
    {
        request.Result = page;
        _lastRequest = request;
        OnAfterRequest?.Invoke(request);
        return ConfigureResult(page);
    }

    public void RestoreLastRequest(FluxPageRequestRecord<TModel>? request)
    {
        if (request is null) return;
        _lastRequest = request;
    }

    protected virtual GridItemsProviderResult<TModel> ConfigureResult(PageResult<TModel> page)
    {
        return GridItemsProviderResult.From(items: page.Data!.ToList(), totalItemCount: page.Total!.Value);
    }

    protected FluxSortingInfo GetSorting(GridItemsProviderRequest<TModel> request)
    {
        var expression = GetSortingExpression(request);
        if (expression is null) return new FluxSortingInfo();

        var sortValue = SortMap.GetSortValue(expression);
        var direction = request.SortByAscending ? OrderDirection.Ascending : OrderDirection.Descending;

        return new FluxSortingInfo(sortValue, direction);
    }

    private LambdaExpression? GetSortingExpression(GridItemsProviderRequest<TModel> request)
    {
        var sortingColumn = request.SortByColumn;
        if (sortingColumn is null) return null;

        var type = sortingColumn.GetType();
        var fieldInfo = type.GetProperty("Property", BindingFlags.Public | BindingFlags.Instance)!;
        var sorting = fieldInfo.GetValue(sortingColumn) as LambdaExpression;

        return sorting;
    }

    protected ICollection<KeyValuePair<string, string>> NewQuery() => [];

}
