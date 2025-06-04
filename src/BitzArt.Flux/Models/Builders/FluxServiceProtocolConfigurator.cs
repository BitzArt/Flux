namespace BitzArt.Flux.Builder;

internal class FluxServiceProtocolConfigurator : IFluxServiceProtocolConfigurator
{
    public IFluxBuilder FluxBuilder { get; private init; }
    public string ServiceName { get; private init; }

    public FluxServiceProtocolConfigurator(IFluxBuilder fluxBuilder, string name)
    {
        FluxBuilder = fluxBuilder;
        ServiceName = name;
    }
}
