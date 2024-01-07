using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxServicePreBuilder : IFluxServicePreBuilder
{
    public string? Name { get; set; }
    public IServiceCollection Services { get; init; }
    public IFluxFactory Factory { get; init; }

    public FluxServicePreBuilder(IServiceCollection services, IFluxFactory factory, string? name)
    {
        Services = services;
        Factory = factory;
        Name = name;
    }
}
