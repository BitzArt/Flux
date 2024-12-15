namespace BitzArt.Flux.MudBlazor;

public delegate void OnItemsIndexedHandler<TModel>(OnItemsIndexedEventArgs<TModel> args)
    where TModel : class;


public class OnItemsIndexedEventArgs<TModel>(object sender, IDictionary<TModel, int> indexMap) : EventArgs
    where TModel : class
{
    public object Sender { get; } = sender;

    public IDictionary<TModel, int> IndexMap { get; } = indexMap;
}
