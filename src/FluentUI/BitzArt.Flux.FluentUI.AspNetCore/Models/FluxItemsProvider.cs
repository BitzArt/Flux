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

    public FluxItemsProvider(IFluxSetContext<TModel> fluxSet, PaginationState paginationState)
    {
        SortMap = new();
        _fluxSet = fluxSet;
        PaginationState = paginationState;
    }

    private async ValueTask<GridItemsProviderResult<TModel>> GetItemsAsync(GridItemsProviderRequest<TModel> request)
    {
        // var cachedResult = GetCachedResult(request);
        // if (cachedResult is not null)
        //     return GridItemsProviderResult.From(items: cachedResult.Data!.ToList(), totalItemCount: cachedResult.Total);

        var pageRequest = new PageRequest(request.StartIndex, request.Count);

        pageRequest = await ConfigurePageRequestAsync(pageRequest);
        var sort = GetSorting(request);
        var parameters = await ConfigureParametersAsync(sort, request);

        var page = await _fluxSet!.GetPageAsync(
            pageRequest,
            //request.CancellationToken,
            parameters: parameters);

        return ConfigureResult(page);
    }

    protected virtual Task<PageRequest> ConfigurePageRequestAsync(PageRequest pageRequest) => Task.FromResult(pageRequest);

    protected virtual Task<object[]> ConfigureParametersAsync(FluxSortingInfo sort, GridItemsProviderRequest<TModel> request)
        => Task.FromResult(Array.Empty<object>());

    protected virtual GridItemsProviderResult<TModel> ConfigureResult(PageResult<TModel> page)
        => GridItemsProviderResult.From(items: page.Data!.ToList(), totalItemCount: page.Total!.Value);

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
