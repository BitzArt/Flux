using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring a JSON Flux service.
/// </summary>
public static class UsingJsonExtension
{
    /// <summary>
    /// Implements a JSON Flux service.
    /// </summary>
    /// <param name="prebuilder"></param>
    /// <returns>
    /// The <see cref="IFluxJsonServiceBuilder"/> for further service configuration.
    /// </returns>
    public static IFluxJsonServiceBuilder UsingJson(this IFluxServiceBuilder prebuilder)
    {
        prebuilder.ServiceCollection.AddLogging();

        var builder = new FluxJsonServiceBuilder(prebuilder);

        var fluxServiceProvider = builder.Registration;
        builder.ServiceRegistration.ServiceRegistrations.Add(fluxServiceProvider);

        builder.ServiceCollection.AddScoped<IFluxServiceContext>(x =>
        {
            return new FluxServiceContext(fluxServiceProvider, x);
        });

        return builder;
    }
}