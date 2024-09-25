using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxBuilder(IServiceCollection services) : IFluxBuilder
{
    public IServiceCollection Services { get; private set; } = services;
    public IFluxFactory Factory { get; init; } = new FluxFactory();
}
