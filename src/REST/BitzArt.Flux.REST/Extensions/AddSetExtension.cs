using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

/// <summary>
/// Extension methods for configuring set contexts with an <see cref="IFluxServiceBuilder"/>.
/// </summary>
public static class AddSetExtension
{
    /// <inheritdoc cref="AddSet{TModel, TKey}(IFluxRestServiceBuilder, object?, string?, ServiceLifetime)"/>
    public static IFluxRestSetBuilder<TModel, object> AddSet<TModel>(this IFluxRestServiceBuilder serviceBuilder, object? setKey = null, string? path = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TModel : class
        => AddSet<TModel, object>(serviceBuilder, setKey, path, lifetime);

    /// <summary>
    /// Configures a set context for the specified model type and key type.
    /// </summary>
    /// <typeparam name="TModel">Set model type.</typeparam>
    /// <typeparam name="TKey">Set key type.</typeparam>
    /// <param name="serviceBuilder">Flux service builder to register the set with.</param>
    /// <param name="setKey">Set key (if any).</param>
    /// <param name="path">Set path part (if any), relative to the service base path.</param>
    /// <param name="lifetime">Set context lifetime.</param>
    /// <returns><see cref="IFluxRestSetBuilder{TModel, TKey}"/> for further configuration.</returns>"/>
    public static IFluxRestSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxRestServiceBuilder serviceBuilder, object? setKey = null, string? path = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TModel : class
        where TKey : notnull
    {
        var serviceCollection = serviceBuilder.ServiceCollection;
        var builder = new SetBuilder<TModel, TKey>(serviceBuilder, path);

        serviceCollection.AddSetContext(serviceBuilder.ServiceName, setKey, lifetime,
            serviceProvider => new SetContext<TModel, TKey>(builder.SetConfiguration));

        return builder;
    }
}
