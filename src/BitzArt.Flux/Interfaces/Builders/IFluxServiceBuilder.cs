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
    public IServiceCollection ServiceCollection { get; }

    /// <summary>
    /// Flux Service Factory instance.
    /// </summary>
    public IFluxServiceRegistration ServiceFactory { get; }

    /// <summary>
    /// Flux Factory instance.
    /// </summary>
    public IFluxFactory ServiceRegistration { get; }
}