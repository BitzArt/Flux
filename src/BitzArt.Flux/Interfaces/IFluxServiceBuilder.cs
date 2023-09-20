using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxServiceBuilder
{
    internal IServiceCollection Services { get; }
    internal IFluxServiceProvider ServiceContext { get; }
    internal IFluxProvider Factory { get; }
}