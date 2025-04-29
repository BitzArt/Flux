using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Flux Builder instance.
/// See <see href="https://bitzart.github.io/Flux/02.configure.html">Configure Flux</see> for more information.
/// </summary>
public interface IFluxBuilder
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> instance this <see cref="IFluxBuilder"/> is using.
    /// </summary>
    public IServiceCollection ServiceCollection { get; }

    /// <summary>
    /// <para>
    /// Notifies the builder that a service has been added.
    /// </para>
    /// <para>
    /// This is a part of internal implementation details and should not be used directly. <br />
    /// It is only exposed for the purposes of <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementations</see>.
    /// </para>
    /// </summary>
    /// <param name="name">Name of the added service.</param>
    public void OnServiceAdded(string name);

    /// <summary>
    /// <para>
    /// Notifies the builder that a service has been terminated.
    /// </para>
    /// <para>
    /// This is a part of internal implementation details and should not be used directly. <br />
    /// It is only exposed for the purposes of <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementations</see>.
    /// </para>
    /// </summary>
    /// <param name="name">Name of the terminated service.</param>
    /// <param name="terminatedBuilder">Builder instance created as a result of the termination.</param>
    public void OnServiceTerminated(string name, IFluxServiceBuilder terminatedBuilder);
}
