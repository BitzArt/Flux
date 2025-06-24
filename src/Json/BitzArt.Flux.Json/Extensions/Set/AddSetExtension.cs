using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for configuring set contexts with an <see cref="IFluxServiceBuilder"/>.
/// </summary>
public static class AddSetExtension
{
    /// <summary>
    /// Configures a set context for the specified model type and key type.
    /// </summary>
    /// <typeparam name="TModel">Set model type.</typeparam>
    /// <typeparam name="TKey">Set key type.</typeparam>
    /// <param name="serviceBuilder">Flux service builder to register the set with.</param>
    /// <param name="setKey">Set key (if any).</param>
    /// <param name="lifetime">Set context lifetime.</param>
    /// <returns><see cref="IFluxJsonSetBuilder{TModel, TKey}"/> for further configuration.</returns>"/>
    public static IFluxJsonSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxJsonServiceBuilder serviceBuilder, object? setKey = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TModel : class
        where TKey : notnull
    {
        var serviceCollection = serviceBuilder.ServiceCollection;
        var builder = new FluxJsonSetBuilder<TModel, TKey>(serviceBuilder);

        serviceCollection.AddSetContext(serviceBuilder.ServiceName, setKey, lifetime,
            (serviceProvider, logger) =>
            {
                return new FluxJsonSetContext<TModel, TKey>(builder.SetConfiguration, serviceProvider, logger);
            });

        return builder;
    }
}
