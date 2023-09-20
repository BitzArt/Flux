using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxBuilder
{
    internal IFluxProvider Factory { get; }
    internal IServiceCollection Services { get; }
}
