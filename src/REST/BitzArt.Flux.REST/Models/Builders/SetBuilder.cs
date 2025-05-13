namespace BitzArt.Flux.REST;

internal class SetBuilder<TModel, TKey> : IFluxRestSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxRestServiceBuilder ServiceBuilder { get; set; }
    public SetConfiguration SetConfiguration { get; set; }

    public SetBuilder(IFluxRestServiceBuilder serviceBuilder, string? path = null)
    {
        ServiceBuilder = serviceBuilder;
        SetConfiguration = new(ServiceBuilder.ServiceConfiguration, path);
    }

    public string ImplementationName => "REST";
}
