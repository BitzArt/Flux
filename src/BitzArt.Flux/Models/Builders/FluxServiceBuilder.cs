namespace BitzArt.Flux.Builder;

internal class FluxServiceBuilder : IFluxServiceBuilder
{
    public IFluxBuilder FluxBuilder { get; private init; }
    public string ServiceName { get; private init; }

    public FluxServiceBuilder(IFluxBuilder fluxBuilder, string name)
    {
        FluxBuilder = fluxBuilder;
        ServiceName = name;
    }
}
