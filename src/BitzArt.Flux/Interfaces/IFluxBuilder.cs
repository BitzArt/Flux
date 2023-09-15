using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxBuilder
{
    internal IFluxServiceFactory Factory { get; }
    internal IServiceCollection Services { get; }
}
