using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxServiceBuilder
{
    internal IServiceCollection Services { get; }
    internal IFluxServiceProvider ServiceProvider { get; }
    internal IFluxProvider Provider { get; }
}