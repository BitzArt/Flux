namespace BitzArt.Flux.MudBlazor;

/// <summary>
/// Handler for an event triggered when a request was completed and resulting items were indexed.
/// </summary>
public delegate void OnItemsIndexedHandler<TModel>(OnItemsIndexedEventArgs<TModel> args)
    where TModel : class;


/// <summary>
/// Event arguments for an event triggered when a request was completed and resulting items were indexed.
/// </summary>
/// <typeparam name="TModel"></typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="OnItemsIndexedEventArgs{TModel}"/> class.
/// </remarks>
public class OnItemsIndexedEventArgs<TModel>(object sender, IDictionary<TModel, int> itemIndexMap) : EventArgs
    where TModel : class
{
    /// <summary>
    /// Object that triggered the event.
    /// </summary>
    public object Sender { get; } = sender;

    /// <summary>
    /// Map of item indices.
    /// </summary>
    public IDictionary<TModel, int> ItemIndexMap { get; } = itemIndexMap;
}
