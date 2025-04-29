namespace BitzArt.Flux.REST;

internal class FluxRestSetBuilder<TModel, TKey> : IFluxRestSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxRestServiceBuilder ServiceBuilder { get; set; }

    public FluxRestSetBuilder(IFluxRestServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
    }
}
