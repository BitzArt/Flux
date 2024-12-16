using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace BitzArt.Flux.MudBlazor;

// TODO: ? Extract reset logic ?
// TODO: ? Extract page state comparison logic ?
// TODO: Cleanup and refactor
// TODO: Forward CancellationToken

internal class FluxSetDataProvider<TModel>(ILoggerFactory loggerFactory) : IFluxSetDataProvider<TModel>
    where TModel : class
{
    private readonly ILogger _logger = loggerFactory.CreateLogger("Flux.MudBlazor");

    private static readonly FieldInfo _tableCurrentPageField = typeof(MudTableBase)
        .GetField("_currentPage", BindingFlags.NonPublic | BindingFlags.Instance)!;

    public IFluxSetContext<TModel> SetContext { get; internal set; } = null!;

    public Func<TableState, CancellationToken, Task<TableData<TModel>>> Data => GetDataAsync;

    public Func<TableState, object[]>? GetParameters { get; set; } = null;

    public event OnResultHandler<TModel>? OnResult;

    public FluxSetDataPageQuery<TModel>? LastQuery { get; set; }

    public bool IsLoading { get; private set; }

    public event OnLoadingStateChanged<TModel>? OnLoadingStateChanged;

    private int _currentOperationCount = 0;

    private bool _resetting = false;

    private bool _resetPageOnce = false;

    public int DefaultPageSize { get; set; } = 10;

    public TableState DefaultTableState => new() { Page = 0, PageSize = DefaultPageSize };

    public TableState TableState
    {
        get
        {
            if (Table is not null) return new TableState
            {
                Page = Table.CurrentPage,
                PageSize = Table.RowsPerPage,
                SortDirection = Table.Context.SortDirection,
                SortLabel = Table.Context.CurrentSortLabel?.SortLabel
            };

            return DefaultTableState;
        }
    }

    public void RestoreLastQuery(object query)
    {
        if (query is not FluxSetDataPageQuery<TModel> lastQuery) return;
        LastQuery = lastQuery;
    }

    public async Task ResetAndReloadAsync(bool ignoreCancellation = true)
    {
        ResetPage();
        await ResetSortAndReloadAsync(ignoreCancellation);
    }

    public async Task ResetSortAndReloadAsync(bool ignoreCancellation = true)
    {
        if (Table is null) throw new InvalidOperationException(
            "Table component must be forwarded to the flux data provider for it to be able to reset sorting.");

        foreach (var sortLabel in Table.Context.SortLabels)
        {
            sortLabel.SortDirection = SortDirection.None;
        }

        if (Table.Context.CurrentSortLabel is not null)
        {
            await Table.Context.SetSortFunc(Table.Context.CurrentSortLabel).IgnoreCancellation();
        }
        else
        {
            await ReloadTableAsync(ignoreCancellation);
        }
    }

    public async Task ResetPageAndReloadAsync(bool ignoreCancellation = true)
    {
        ResetPage();
        await ReloadTableAsync(ignoreCancellation);
    }

    private async Task ReloadTableAsync(bool ignoreCancellation)
    {
        if (Table is null) throw new InvalidOperationException(
            "Table component must be forwarded to the flux data provider for it to be able to trigger a reload.");

        await Table!.ReloadServerData().IgnoreCancellation(ignoreCancellation);
    }

    public void ResetPage()
    {
        _resetPageOnce = true;
    }

    public Func<bool>? ShouldResetPage { get; set; }

    public bool ShouldResetPageOnOrderChanged { get; set; } = true;

    public bool ShouldResetPageOnOrderDirectionChanged { get; set; } = true;

    public Func<object[], object[], bool>? ShouldResetPageOnParameters { get; set; } = null;

    public MudTable<TModel>? Table { get; set; }

    public async Task<TableData<TModel>> GetDataAsync(CancellationToken cancellationToken = default)
        => await GetDataAsync(TableState, cancellationToken);

    public async Task<TableData<TModel>> GetDataAsync(TableState state, CancellationToken cancellationToken = default)
    {
        await AddOperationAsync();

        try
        {
            var result = await GetDataInternalAsync(state, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            return result;
        }
        finally
        {
            await RemoveOperationAsync();
        }
    }

    private async Task AddOperationAsync()
    {
        _currentOperationCount++;

        var loadingStateChanged = UpdateLoading(true);
        if (loadingStateChanged && OnLoadingStateChanged is not null)
            await OnLoadingStateChanged.Invoke(new(this));
    }

    private async Task RemoveOperationAsync()
    {
        _currentOperationCount--;
        if (_currentOperationCount > 0) return;

        var loadingStateChanged = UpdateLoading(false);
        if (loadingStateChanged && OnLoadingStateChanged is not null)
            await OnLoadingStateChanged.Invoke(new(this));
    }

    private bool UpdateLoading(bool newValue)
    {
        if (IsLoading == newValue) return false;

        IsLoading = newValue;
        return true;
    }

    private async Task<TableData<TModel>> GetDataInternalAsync(TableState state, CancellationToken cancellationToken)
    {
        var parameters = GetParameters is not null ? GetParameters(state) : [];

        if (ShouldReset(state, parameters))
        {
            if (Table is null) throw new InvalidOperationException(
                "Table component must be forwarded to the flux data provider for it to be able to reset current page.");

            // Not using public CurrentPage property due to
            // the side effect of it triggering a table reload.
            _tableCurrentPageField.SetValue(Table, 0);

            _resetting = true;
            _logger.LogDebug("Resetting page for {Model} data provider.", typeof(TModel).Name);

            await Table.ReloadServerData();

            throw new OperationCanceledException("Page reset");
        }

        if (_resetting == true)
        {
            _resetting = false;
            _logger.LogDebug("Processing reset for {Model} data provider.", typeof(TModel).Name);
        }

        if (CompareWithLastRequest(state, parameters)) return LastQuery!.Result;

        var currentQuery = new FluxSetDataPageQuery<TModel>()
        {
            TableState = state,
            Parameters = parameters
        };
        var page = await SetContext.GetPageAsync(state.Page * state.PageSize, state.PageSize, parameters: parameters);
        var result = BuildTableData(page, currentQuery);

        if (IndexItems)
        {
            UpdateItemIndexMap(result.Items!);
            OnItemsIndexed?.Invoke(new(this, ItemIndexMap!));
        }

        LastQuery = currentQuery;
        OnResult?.Invoke(new(this, LastQuery));

        return result;
    }

    private bool ShouldReset(TableState state, object[] newParameters)
    {
        // already resetting, do not loop infinitely
        if (_resetting) return false;

        // manual page reset requested
        if (_resetPageOnce)
        {
            _resetPageOnce = false;
            return true;
        }

        // reset on order change
        if (ShouldResetOrderChanged(state)) return true;

        // reset on order direction change
        if (ShouldResetOrderDirectionChanged(state)) return true;

        // dynamic reset
        if (ShouldResetDynamic()) return true;

        // dynamic reset based on parameters
        if (ShouldResetDynamicOnParameters(newParameters)) return true;

        // reset is not requested
        return false;
    }

    private bool ShouldResetOrderChanged(TableState newState) =>
        ShouldResetPageOnOrderChanged
        && LastQuery is not null
        && HasOrderChanged(LastQuery!.TableState, newState);

    private bool ShouldResetOrderDirectionChanged(TableState newState) =>
        ShouldResetPageOnOrderDirectionChanged
        && LastQuery is not null
        && HasOrderDirectionChanged(LastQuery.TableState, newState);

    private bool ShouldResetDynamic() =>
        ShouldResetPage is not null && ShouldResetPage.Invoke() == true;

    private bool ShouldResetDynamicOnParameters(object[] newParameters)
    {
        var lastParameters = LastQuery?.Parameters;

        return lastParameters is not null
            && ShouldResetPageOnParameters is not null
            && ShouldResetPageOnParameters!.Invoke(lastParameters, newParameters) == true;
    }


    private static bool HasOrderChanged(TableState lastState, TableState newState)
    {
        // sort label (column) has changed
        if (lastState.SortLabel != newState.SortLabel) return true;

        // no change detected
        return false;
    }

    private static bool HasOrderDirectionChanged(TableState lastState, TableState newState)
    {
        // sort direction has changed
        if (lastState.SortDirection != newState.SortDirection) return true;

        // no change detected
        return false;
    }

    private bool CompareWithLastRequest(TableState newState, object[] newParameters)
    {
        // no last query, no comparison
        if (LastQuery is null) return false;

        // state has changed
        var pageStateHasChanged = ComparePageStates(LastQuery!.TableState, newState) == false;
        if (pageStateHasChanged) return false;

        // parameters have changed
        var parametersHaveChanged = CompareParameters(LastQuery!.Parameters, newParameters) == false;
        if (parametersHaveChanged) return false;

        // no change detected
        return true;
    }

    private static bool ComparePageStates(TableState lastState, TableState newState)
    {
        // page index has changed
        if (lastState.Page != newState.Page) return false;

        // page size has changed
        if (lastState.PageSize != newState.PageSize) return false;

        // no change detected
        return true;
    }

    private static bool CompareParameters(object[]? lastParameters, object[] newParameters)
    {
        // no last parameters, no comparison
        if (lastParameters is null) return false;

        // different number of parameters
        if (lastParameters.Length != newParameters.Length) return false;

        // compare each parameter
        for (var i = 0; i < lastParameters.Length; i++)
        {
            if (!lastParameters[i].Equals(newParameters[i])) return false;
        }

        // no change detected
        return true;
    }

    // TODO: Extract as extension method ?
    private static TableData<TModel> BuildTableData(PageResult<TModel> page, FluxSetDataPageQuery<TModel> currentQuery)
    {
        var result = new TableData<TModel>()
        {
            Items = page.Data,
            TotalItems = page.Total!.Value
        };

        currentQuery.Result = result;

        return result;
    }

    public bool IndexItems { get; set; } = false;

    public IDictionary<TModel, int>? ItemIndexMap { get; set; }

    public event OnItemsIndexedHandler<TModel>? OnItemsIndexed;

    public int IndexOf(TModel item)
    {
        if (!IndexItems)
            throw new InvalidOperationException(
                "'IndexItems' should be set to 'true' before attempting to retrieve the index of an item.");

        if (ItemIndexMap is null)
            throw new InvalidOperationException(
                "'ItemIndexMap' is null. Ensure items are indexed before attempting to retrieve the index of an item.");

        try
        {
            return ItemIndexMap[item];
        }
        catch
        {
            return -1;
        }
    }

    private void UpdateItemIndexMap(IEnumerable<TModel> newItems)
    {
        if (!IndexItems)
            throw new UnreachableException();

        var newItemsCount = newItems.Count();

        if (ItemIndexMap is null)
        {
            ItemIndexMap = CreateItemIndexMap(newItems, newItemsCount);
            return;
        }

        var lastItems = LastQuery?.Result.Items;
        var lastItemsCount = lastItems is not null ? lastItems.Count() : 0;

        if (newItemsCount > lastItemsCount)
        {
            // recreate the item index map with increased size
            ItemIndexMap = CreateItemIndexMap(newItems, newItemsCount);
            return;
        }

        ItemIndexMap.Clear();
        PopulateItemIndexMap(ItemIndexMap, newItems);
    }

    private static Dictionary<TModel, int> CreateItemIndexMap(IEnumerable<TModel> items, int mapSize)
    {
        var map = new Dictionary<TModel, int>(mapSize);
        PopulateItemIndexMap(map, items);
        return map;
    }

    private static void PopulateItemIndexMap(IDictionary<TModel, int> map, IEnumerable<TModel> items)
    {
        int index = 0;
        foreach (var item in items)
        {
            map.Add(item, index);
            index++;
        }
    }
}
