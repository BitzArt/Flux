using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxServiceBuilder
{
    internal IServiceCollection Services { get; }
    internal IFluxServiceFactory ServiceFactory { get; }
    internal IFluxFactory Factory { get; }
}