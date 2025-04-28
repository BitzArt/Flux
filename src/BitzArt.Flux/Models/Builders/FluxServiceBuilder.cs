using BitzArt.Flux.Services;

namespace BitzArt.Flux.Builder;

internal class FluxServiceBuilder : IFluxServiceBuilder, ITerminatable
{
    private bool _isTerminated;

    public IFluxBuilder FluxBuilder { get; private init; }
    public string ServiceName { get; private init; }

    public FluxServiceBuilder(IFluxBuilder fluxBuilder, string name)
    {
        FluxBuilder = fluxBuilder;
        ServiceName = name;
    }

    public void Terminate()
    {
        if (_isTerminated)
            throw new InvalidOperationException("The service builder has already previously been terminated.");

        _isTerminated = true;
    }
}
