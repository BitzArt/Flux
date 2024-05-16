using BitzArt.Pagination;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace BitzArt.Flux;

/// <summary>
/// Provides items for a grid using Flux.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <inheritdoc/>
public class FluxItemsProvider<TModel> : IFluxItemsProvider<TModel>
    where TModel : class
{
    private readonly IFluxContext _flux;
    private IFluxSetContext<TModel>? _fluxSet;

    /// <inheritdoc />
    public readonly PaginationState PaginationState;

    /// <inheritdoc />
    public int CurrentPage => PaginationState.CurrentPageIndex + 1;

    /// <inheritdoc />
    public int TotalItems => PaginationState.TotalItemCount ?? 0;

    /// <inheritdoc />
    public int TotalPages => PaginationState.LastPageIndex.HasValue ? PaginationState.LastPageIndex!.Value + 1 : 0;

    /// <inheritdoc />
    public int PageSize => PaginationState.ItemsPerPage;

    /// <inheritdoc />
    public GridItemsProvider<TModel> GetItems => new(GetItemsAsync);

    private FluxPageRequestRecord<TModel>? _lastRequest = null;

    /// <inheritdoc />
    public event IFluxItemsProvider<TModel>.OnAfterRequestHandler? OnAfterRequest;

    /// <summary>
    /// Override this to configure the default page size.
    /// </summary>
    protected virtual int DefaultPageSize => 10;

    /// <summary>
    /// Creates a new instance of <see cref="FluxItemsProvider{TModel}"/>.
    /// </summary>
    /// <param name="flux"></param>
    public FluxItemsProvider(IFluxContext flux)
    {
        _flux = flux;
        PaginationState = new PaginationState() { ItemsPerPage = DefaultPageSize };
    }

    private void EnsureSet()
    {
        _fluxSet ??= _flux.Set<TModel>();
    }

    /// <inheritdoc />
    public void ConfigureSet(IFluxSetContext<TModel> set)
    {
        _fluxSet = set;
    }

    private async ValueTask<GridItemsProviderResult<TModel>> GetItemsAsync(GridItemsProviderRequest<TModel> request)
    {
        EnsureSet();

        var pageRequest = new PageRequest(request.StartIndex, request.Count);

        pageRequest = await ConfigurePageRequestAsync(pageRequest);
        var sort = GetSorting(request);
        if (_lastRequest is not null
            && _lastRequest.Request.Sorting.Compare(sort) == false
            && (!_lastRequest.IsExhausted.HasValue || _lastRequest.IsExhausted.Value == false))
        {
            _lastRequest.IsExhausted = true;
            _ = ResetPaginationAsync();

            Console.WriteLine("Restoring from last result, marking last result as exhausted.");

            return GridItemsProviderResult.From(
                items: _lastRequest.Result!.Data!.ToList(),
                totalItemCount: _lastRequest.Result.Total!.Value);
        }

        var parameters = await ConfigureParametersAsync(sort, request);

        var currentRequest = new FluxPageRequestRecord<TModel>(new(pageRequest, sort, parameters));
        if (_lastRequest is not null
            && (!_lastRequest.IsExhausted.HasValue || _lastRequest.IsExhausted.Value == false)
            && _lastRequest.Request.Compare(currentRequest.Request) == true
            && _lastRequest.Result is not null)
        {
            Console.WriteLine("Result was restored from last request.");
            return FinalizeResult(currentRequest, _lastRequest.Result!);
        }

        if (request.CancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("Request was cancelled");
            return GridItemsProviderResult.From(Enumerable.Empty<TModel>().ToList(), 0);
        }

        var page = await _fluxSet!.GetPageAsync(
            pageRequest,
            //request.CancellationToken,
            parameters: parameters);

        Console.WriteLine("Request was made.");

        return FinalizeResult(currentRequest, page);
    }

    /// <summary>
    /// Override this to configure the page request.
    /// </summary>
    protected virtual Task<PageRequest> ConfigurePageRequestAsync(PageRequest pageRequest) => Task.FromResult(pageRequest);

    /// <summary>
    /// Override this to configure the parameters for the request.
    /// </summary>
    protected virtual Task<object[]> ConfigureParametersAsync(FluxSortingInfo sort, GridItemsProviderRequest<TModel> request)
        => Task.FromResult(Array.Empty<object>());

    private GridItemsProviderResult<TModel> FinalizeResult(FluxPageRequestRecord<TModel> request, PageResult<TModel> page)
    {
        request.Result = page;
        _lastRequest = request;
        OnAfterRequest?.Invoke(request);

        return ConfigureResult(page);
    }

    /// <inheritdoc />
    public void RestoreLastRequest(FluxPageRequestRecord<TModel>? request)
    {
        if (request is null) return;
        _lastRequest = request;
    }

    /// <summary>
    /// Override this to configure the result.
    /// </summary>
    protected virtual GridItemsProviderResult<TModel> ConfigureResult(PageResult<TModel> page)
    {
        return GridItemsProviderResult.From(items: page.Data!.ToList(), totalItemCount: page.Total!.Value);
    }

    /// <summary>
    /// Builds the <see cref="FluxSortingInfo"/> from a request.
    /// </summary>
    protected FluxSortingInfo GetSorting(GridItemsProviderRequest<TModel> request)
    {
        var sortValue = GetSortingValue(request);
        if (sortValue is null) return new FluxSortingInfo();

        var direction = request.SortByAscending ? OrderDirection.Ascending : OrderDirection.Descending;

        return new FluxSortingInfo(sortValue, direction);
    }

    private static object? GetSortingValue(GridItemsProviderRequest<TModel> request)
    {
        var sortingColumn = request.SortByColumn;
        if (sortingColumn is null) return null;

        var type = sortingColumn.GetType();

        // If column inherits from SortMapPropertyColumn, return it's SortValue.
        if (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISortMapColumn<>)))
        {
            var sortValuePropertyInfo = type.GetProperty("SortValue", BindingFlags.Public | BindingFlags.Instance)!;
            return sortValuePropertyInfo.GetValue(sortingColumn);
        }

        // Otherwise, return the Property expression.
        var fieldInfo = type.GetProperty("Property", BindingFlags.Public | BindingFlags.Instance)!;
        return fieldInfo.GetValue(sortingColumn) as LambdaExpression;
    }

    /// <inheritdoc />
    public async Task ResetPaginationAsync()
    {
        await SetPageAsync(1);
    }

    /// <inheritdoc />
    public async Task SetPageAsync(int pageNumber)
    {
        if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0.");
        await PaginationState.SetCurrentPageIndexAsync(pageNumber - 1);
    }
}
