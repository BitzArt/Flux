using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxServiceBuilder
{
    internal IServiceCollection Services { get; }
    internal IFluxServiceContext ServiceContext { get; }
    internal IFluxServiceFactory Factory { get; }
}