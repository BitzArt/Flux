using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxServicePreBuilder : IFluxServicePreBuilder
{
    public string? Name { get; set; }
    public IServiceCollection Services { get; init; }
    public IFluxProvider Factory { get; init; }

    public FluxServicePreBuilder(IServiceCollection services, IFluxProvider factory, string? name)
    {
        Services = services;
        Factory = factory;
        Name = name;
    }
}
