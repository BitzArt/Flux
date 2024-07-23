using MudBlazor;

namespace BitzArt.Flux.MudBlazor;

public interface IFluxSetDataProvider<TModel>
    where TModel : class
{
    public Func<TableState, CancellationToken, Task<TableData<TModel>>> Data { get; }
    public Func<TableState, object[]>? GetParameters { get; set; }
    public MudTable<TModel>? Table { get; set; }

    /// <summary>
    /// Resets current page to 0 on next request.
    /// </summary>
    public void ResetPage();
    
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

    /// <summary>
    /// Resets current page to 0 and reloads the data.
    /// </summary>
    /// <returns></returns>
    public Task ResetAndReloadAsync();
}