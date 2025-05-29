namespace BitzArt.Flux.Json;

internal class FluxJsonSetBuilder<TModel, TKey> : IFluxJsonSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxJsonServiceBuilder ServiceBuilder { get; set; }
    public FluxJsonSetConfiguration SetConfiguration { get; set; }

    public FluxJsonSetBuilder(IFluxJsonServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
        SetConfiguration = new(ServiceBuilder.ServiceConfiguration);
    }
}
