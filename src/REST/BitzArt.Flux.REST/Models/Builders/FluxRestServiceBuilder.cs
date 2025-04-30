namespace BitzArt.Flux.REST;

internal class FluxRestServiceBuilder : IFluxRestServiceBuilder
{
    public IFluxBuilder FluxBuilder { get; set; }
    public string ServiceName { get; set; }
    public FluxRestServiceConfiguration ServiceConfiguration { get; set; }

    public FluxRestServiceBuilder(IFluxBuilder fluxBuilder, string serviceName, string? baseUrl)
    {
        FluxBuilder = fluxBuilder;
        ServiceName = serviceName;
        ServiceConfiguration = new(baseUrl);
    }
}
