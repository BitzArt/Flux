using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Flux Service Builder instance.
/// See <see href="https://bitzart.github.io/Flux/02.configure.html">Configure Flux</see> for more information.
/// </summary>
public interface IFluxServiceBuilder
{
    internal IServiceCollection Services { get; }
    internal IFluxServiceFactory ServiceFactory { get; }
    internal IFluxFactory Factory { get; }
}