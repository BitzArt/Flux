using BitzArt.Pagination;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System.Reflection;

namespace BitzArt.Flux.MudBlazor;

internal class FluxSetDataProvider<TModel>(ILoggerFactory loggerFactory) : IFluxSetDataProvider<TModel>
    where TModel : class
{
    private readonly ILogger _logger = loggerFactory.CreateLogger("Flux.MudBlazor");

    private static readonly FieldInfo _tableCurrentPageField = typeof(MudTableBase)
        .GetField("_currentPage", BindingFlags.NonPublic | BindingFlags.Instance)!;

    public IFluxSetContext<TModel> SetContext { get; internal set; } = null!;

    public Func<TableState, CancellationToken, Task<TableData<TModel>>> Data => GetDataAsync;

    public Func<TableState, IOperationParameterCollection>? GetParameters { get; set; } = null;

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

    public Func<IOperationParameterCollection?, IOperationParameterCollection?, bool>? ShouldResetPageOnParameters { get; set; } = null;

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
        IOperationParameterCollection? parameters = GetParameters?.Invoke(state);

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

        if (CompareWithLastRequest(state, parameters))
            return LastQuery!.Data.ToTableData();

        var pageRequest = new PageRequest(state.Page * state.PageSize, state.PageSize);
        var page = await SetContext.GetPageAsync(pageRequest, parameters: parameters is not null ? new(parameters) : null, cancellationToken);

        LastQuery = new(state, parameters!, page);
        OnResult?.Invoke(new(this, LastQuery));

        return page.ToTableData();
    }

    private bool ShouldReset(TableState state, IOperationParameterCollection? newParameters)
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

    private bool ShouldResetDynamicOnParameters(IOperationParameterCollection? newParameters)
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

    private bool CompareWithLastRequest(TableState newState, IOperationParameterCollection? newParameters)
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

    private static bool CompareParameters(IOperationParameterCollection? lastParameters, IOperationParameterCollection? newParameters)
    {
        // no last parameters, no comparison
        if (lastParameters is null) return false;

        // no new parameters, no comparison
        if (newParameters is null) return false;

        // different number of parameters
        if (lastParameters.Values.Count() != newParameters.Values.Count()) return false;

        // compare each parameter
        for (var i = 0; i < lastParameters.Values.Count(); i++)
        {
            if (!lastParameters.Values.ElementAt(i).Equals(newParameters.Values.ElementAt(i))) return false;
        }

        // no change detected
        return true;
    }
}
