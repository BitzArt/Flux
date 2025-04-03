using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.Builder;

internal class FluxBuilder(IServiceCollection services) : IFluxBuilder
{
    public IServiceCollection ServiceCollection { get; private set; } = services;
    public IFluxFactory Factory { get; init; } = new FluxFactory();
}
