using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.Builder;

internal class FluxBuilder : IFluxBuilder
{
    public IServiceCollection ServiceCollection { get; private init; }

    public FluxBuilder(IServiceCollection serviceCollection)
    {
        ServiceCollection = serviceCollection;
    }
}
