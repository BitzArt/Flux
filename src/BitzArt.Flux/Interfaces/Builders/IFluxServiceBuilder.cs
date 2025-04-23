using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Flux Service Builder instance.
/// Use a <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementation</see>
/// in order to create an actual Flux Service from this builder.
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

    IServiceCollection IFluxBuilder.ServiceCollection => FluxBuilder.ServiceCollection;
}
