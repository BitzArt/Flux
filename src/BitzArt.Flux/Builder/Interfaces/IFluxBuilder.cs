using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxBuilder
{
    internal IFluxFactory Factory { get; }
    internal IServiceCollection Services { get; }
}
