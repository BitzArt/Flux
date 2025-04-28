namespace BitzArt.Flux.REST;

internal class ServiceBuilder : IFluxRestServiceBuilder
{
    public IFluxServiceBuilder SourceBuilder { get; set; }
    public ServiceOptions ServiceOptions { get; set; }

    public ServiceBuilder(IFluxServiceBuilder sourceBuilder, string? baseUrl)
    {
        SourceBuilder = sourceBuilder;
        ServiceOptions = new(baseUrl);
    }
}
