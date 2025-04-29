using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Flux Service Builder.
/// Use a <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementation</see>
/// in order to configure protocol for the service.
/// </summary>
public interface IFluxServiceProtocolConfigurator : IFluxBuilder
{
    /// <summary>
    /// <see cref="IFluxBuilder"/> instance that was used to create this service builder.
    /// </summary>
    public IFluxBuilder FluxBuilder { get; }

    /// <summary>
    /// Name of the service being configured.
    /// </summary>
    public string ServiceName { get; }

    // ================ Inherited members ================

    IServiceCollection IFluxBuilder.ServiceCollection => FluxBuilder.ServiceCollection;
    void IFluxBuilder.OnServiceAdded(string name) => FluxBuilder.OnServiceAdded(name);
    void IFluxBuilder.OnServiceProtocolConfigured(string name, IFluxServiceBuilder terminatedBuilder) => FluxBuilder.OnServiceProtocolConfigured(name, terminatedBuilder);
}
