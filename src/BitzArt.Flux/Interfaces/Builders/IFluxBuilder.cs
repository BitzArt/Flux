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
}
