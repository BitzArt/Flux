using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Flux Service Builder instance.
/// See <see href="https://bitzart.github.io/Flux/02.configure.html">Configure Flux</see> for more information.
/// </summary>
public interface IFluxServiceBuilder
{
    /// <summary>
    /// Service collection where Flux Services are registered.
    /// </summary>
    public IServiceCollection Services { get; }

    /// <summary>
    /// Flux Service Factory instance.
    /// </summary>
    public IFluxServiceFactory ServiceFactory { get; }

    /// <summary>
    /// Flux Factory instance.
    /// </summary>
    public IFluxFactory Factory { get; }
}