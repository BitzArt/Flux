using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxServiceBuilder
{
    internal IServiceCollection Services { get; }
    internal IFluxServiceProvider Provider { get; }
    internal IFluxServiceFactory Factory { get; }
}