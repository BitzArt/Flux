using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Flux Service Builder.
/// </summary>
public interface IFluxServiceBuilder : IFluxBuilder
{
    /// <summary>
    /// <see cref="IFluxBuilder"/> instance that was used to create this service builder.
    /// </summary>
    public IFluxBuilder FluxBuilder { get; }

    /// <summary>
    /// Name of the service this builder is creating.
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// Name of the implementation this service is using.
    /// </summary>
    public string ImplementationName { get; }

    // ================ Inherited members ================

    IServiceCollection IFluxBuilder.ServiceCollection => FluxBuilder.ServiceCollection;
    void IFluxBuilder.OnServiceAdded(string name) => FluxBuilder.OnServiceAdded(name);
    void IFluxBuilder.OnServiceProtocolConfigured(string name, IFluxServiceBuilder terminatedBuilder) => FluxBuilder.OnServiceProtocolConfigured(name, terminatedBuilder);
}
