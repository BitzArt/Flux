namespace BitzArt.Flux.REST;

internal class FluxRestSetBuilder<TModel, TKey> : IFluxRestSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxRestServiceBuilder ServiceBuilder { get; set; }
    public FluxRestSetConfiguration SetConfiguration { get; set; }

    public FluxRestSetBuilder(IFluxRestServiceBuilder serviceBuilder, string? path = null)
    {
        ServiceBuilder = serviceBuilder;
        SetConfiguration = new(ServiceBuilder.ServiceConfiguration, path);
    }

    public string ImplementationName => "REST";
}
