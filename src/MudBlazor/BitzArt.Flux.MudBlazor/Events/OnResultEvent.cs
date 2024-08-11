namespace BitzArt.Flux.MudBlazor;

/// <summary>
/// Handler for an event triggered when a request was completed and results are available.
/// </summary>
public delegate void OnResultHandler<TModel>(OnResultEventArgs<TModel> args)
    where TModel : class;


/// <summary>
/// Event arguments for an event triggered when a request was completed and results are available.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="OnResultEventArgs{TModel}"/> class.
/// </remarks>
public class OnResultEventArgs<TModel>(object sender, FluxSetDataPageQuery<TModel> query) : EventArgs
    where TModel : class
{
    /// <summary>
    /// Object that triggered the event.
    /// </summary>
    public object Sender { get; } = sender;

    /// <summary>
    /// Query that triggered the event.
    /// </summary>
    public FluxSetDataPageQuery<TModel> Query { get; } = query;
}
