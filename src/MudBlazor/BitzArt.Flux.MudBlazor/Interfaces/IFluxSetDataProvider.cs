using MudBlazor;

namespace BitzArt.Flux.MudBlazor;

/// <summary>
/// Used to provide data to a MudTable component.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public interface IFluxSetDataProvider<TModel>
    where TModel : class
{
    /// <summary>
    /// MudTable component that uses this data provider.
    /// </summary>
    public MudTable<TModel>? Table { get; set; }

    /// <summary>
    /// Function used to get data from the server.
    /// </summary>
    public Func<TableState, CancellationToken, Task<TableData<TModel>>> Data { get; }

    /// <summary>
    /// Can be set to provide parameters for the request.
    /// </summary>
    public Func<TableState, object[]>? GetParameters { get; set; }

    /// <summary>
    /// Event triggered when a request was completed and results are available.
    /// </summary>
    public event OnResultHandler<TModel>? OnResult;

    /// <summary>
    /// Resets current page to 0 on next request.
    /// </summary>
    public void ResetPage();

    /// <summary>
    /// Resets current page to 0 and reloads the data.
    /// </summary>
    /// <returns></returns>
    public Task ResetAndReloadAsync();

    /// <summary>
    /// Dynamically determine whether to reset page when processing a request or not.
    /// </summary>
    public Func<bool>? ShouldResetPage { get; set; }

    /// <summary>
    /// Whether to reset page when table ordering changes.
    /// </summary>
    public bool ShouldResetPageOnOrderChanged { get; set; }

    /// <summary>
    /// Whether to reset page when table ordering direction changes (asc/desc).
    /// </summary>
    public bool ShouldResetPageOnOrderDirectionChanged { get; set; }

    /// <summary>
    /// Dynamically determine whether to reset page when processing a request based on last and new parameters or not.
    /// </summary>
    public Func<object[], object[], bool>? ShouldResetPageOnParameters { get; set; }
}