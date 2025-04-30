using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

/// <summary>
/// Extension methods for configuring set contexts with an <see cref="IFluxServiceBuilder"/>.
/// </summary>
public static class AddSetExtension
{
    /// <inheritdoc cref="AddSet{TModel, TKey}(IFluxRestServiceBuilder, string?, string?, ServiceLifetime)"/>
    public static IFluxRestSetBuilder<TModel, object> AddSet<TModel>(this IFluxRestServiceBuilder serviceBuilder, string? setName = null, string? path = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TModel : class
        => AddSet<TModel, object>(serviceBuilder, setName, path);

    /// <summary>
    /// Configures a set context for the specified model type and key type.
    /// </summary>
    /// <typeparam name="TModel">Set model type.</typeparam>
    /// <typeparam name="TKey">Set key type.</typeparam>
    /// <param name="serviceBuilder">Flux service builder to register the set with.</param>
    /// <param name="setName">Set name (if any).</param>
    /// <param name="path">Set path part (if any), relative to the service base path.</param>
    /// <param name="lifetime">Set context lifetime.</param>
    /// <returns><see cref="IFluxRestSetBuilder{TModel, TKey}"/> for further configuration.</returns>"/>
    public static IFluxRestSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxRestServiceBuilder serviceBuilder, string? setName = null, string? path = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TModel : class
        where TKey : notnull
    {
        var serviceCollection = serviceBuilder.ServiceCollection;
        var builder = new FluxRestSetBuilder<TModel, TKey>(serviceBuilder, path);

        serviceCollection.AddSetContext(serviceBuilder.ServiceName, setName, lifetime,
            serviceProvider => new FluxRestSetContext<TModel, TKey>(builder.SetConfiguration));

        return builder;
    }
}
