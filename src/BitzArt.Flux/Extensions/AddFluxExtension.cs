using BitzArt.Flux.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for adding Flux to an <see cref="IServiceCollection"/>.
/// See <see href="https://bitzart.github.io/Flux/02.configure.html">Configure Flux</see> for more information.
/// </summary>
public static class AddFluxExtension
{
    /// <summary>
    /// Registers Flux with the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to add Flux to.</param>
    /// <param name="configure">
    /// A delegate to configure the <see cref="IFluxBuilder"/>.
    /// </param>
    /// <returns>The <see cref="IServiceCollection"/> instance to allow chaining.</returns>
    public static IServiceCollection AddFlux(this IServiceCollection services, Action<IFluxBuilder> configure)
    {
        var builder = new FluxBuilder(services);
        configure(builder);

        services.TryAddScoped<IFluxContext>(x => new FluxContext(x));

        return services;
    }
}
