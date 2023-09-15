using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxServicePreBuilder : IFluxServicePreBuilder
{
    public string? Name { get; set; }
    public IServiceCollection Services { get; init; }
    public IFluxServiceFactory Factory { get; init; }

    public FluxServicePreBuilder(IServiceCollection services, IFluxServiceFactory factory, string? name)
    {
        Services = services;
        Factory = factory;
        Name = name;
    }
}
