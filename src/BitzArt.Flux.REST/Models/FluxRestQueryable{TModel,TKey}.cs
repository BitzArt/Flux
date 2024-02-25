namespace BitzArt.Flux;

internal class FluxRestQueryable<TModel, TKey> : FluxRestQueryable<TModel>
    where TModel : class
{
    public FluxRestQueryable(FluxRestSetContext<TModel, TKey> setContext) : base(setContext) { }
}
