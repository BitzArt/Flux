namespace BitzArt.Flux.REST;

internal class ServiceBuilder : IFluxRestServiceBuilder
{
    string IFluxServiceBuilder.ImplementationName => "REST";

    public IFluxBuilder FluxBuilder { get; set; }
    public string ServiceName { get; set; }
    public ServiceConfiguration ServiceConfiguration { get; set; }

    public ServiceBuilder(IFluxBuilder fluxBuilder, string serviceName, string? baseUrl)
    {
        FluxBuilder = fluxBuilder;
        ServiceName = serviceName;
        ServiceConfiguration = new(baseUrl);
    }
}
