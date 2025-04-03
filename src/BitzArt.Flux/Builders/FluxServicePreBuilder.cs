using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.Builder;

internal class FluxServicePreBuilder(IServiceCollection services, IFluxFactory factory, string? name)
    : IFluxServicePreBuilder
{
    public string? Name { get; set; } = name;
    public IServiceCollection ServiceCollection { get; init; } = services;
    public IFluxFactory Factory { get; init; } = factory;
}
