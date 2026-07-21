namespace BitzArt.Flux.Json;

internal class FluxJsonServiceBuilder : IFluxJsonServiceBuilder
{
    string IFluxServiceBuilder.ImplementationName => "Json";

    public IFluxBuilder FluxBuilder { get; set; }
    public string ServiceName { get; set; }
    public FluxJsonServiceConfiguration ServiceConfiguration { get; set; }

    public FluxJsonServiceBuilder(IFluxBuilder fluxBuilder, string serviceName, string? baseFilePath)
    {
        FluxBuilder = fluxBuilder;
        ServiceName = serviceName;
        ServiceConfiguration = new(baseFilePath);
    }
}
