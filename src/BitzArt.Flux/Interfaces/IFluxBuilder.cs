using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxBuilder
{
    internal IFluxProvider Provider { get; }
    internal IServiceCollection Services { get; }
}
