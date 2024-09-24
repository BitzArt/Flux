using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Flux Service Builder instance.
/// See <see href="https://bitzart.github.io/Flux/02.configure.html">Configure Flux</see> for more information.
/// </summary>
public interface IFluxServiceBuilder
{
    public IServiceCollection Services { get; }
    public IFluxServiceFactory ServiceFactory { get; }
    public IFluxFactory Factory { get; }
}