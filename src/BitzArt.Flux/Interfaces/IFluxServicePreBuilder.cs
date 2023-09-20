using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public interface IFluxServicePreBuilder
{
    internal string? Name { get; set; }
    internal IServiceCollection Services { get; }
    internal IFluxProvider Factory { get; }
}
