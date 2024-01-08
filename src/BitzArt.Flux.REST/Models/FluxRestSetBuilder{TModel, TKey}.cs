namespace BitzArt.Flux;

internal class FluxRestSetBuilder<TModel, TKey> : FluxRestSetBuilder<TModel>, IFluxRestSetBuilder<TModel, TKey>
    where TModel : class
{
    public new FluxRestSetOptions<TModel, TKey> SetOptions
    {
        get => (FluxRestSetOptions<TModel, TKey>)_setOptions;
        set => _setOptions = value;
    }

    public FluxRestSetBuilder(IFluxRestServiceBuilder serviceBuilder) : base(serviceBuilder)
    {
        SetOptions = new();
    }
}