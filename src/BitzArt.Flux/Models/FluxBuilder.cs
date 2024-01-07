using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxBuilder : IFluxBuilder
{
    public IServiceCollection Services { get; private set; }
    public IFluxFactory Factory { get; init; }

    public FluxBuilder(IServiceCollection services)
    {
        Services = services;
        Factory = new FluxFactory();
    }
}
