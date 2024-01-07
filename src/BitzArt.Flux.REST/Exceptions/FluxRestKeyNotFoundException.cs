namespace BitzArt.Flux;

internal class FluxRestKeyNotFoundException<TModel> : Exception
{
    private static readonly string Msg = $"Unable to find TKey for type '{typeof(TModel).Name}'. Consider specifying a key when registering the set.";
    public FluxRestKeyNotFoundException() : base(Msg) { }
}
