using BitzArt.Flux.REST;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring the page endpoint of a set.
/// </summary>
public static class WithPageEndpointExtension
{
    /// <summary>
    /// Configures the page endpoint for the <see cref="IFluxRestSetBuilder{TModel,TKey}"/>
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type of the set.
    /// </typeparam>
    /// <typeparam name="TKey">
    /// The key type of the set.
    /// </typeparam>
    /// <returns>
    /// The <see cref="IFluxRestSetEndpointBuilder{TModel,TKey}"/> with the page endpoint configured
    /// </returns>
    public static IFluxRestSetEndpointBuilder<TModel, TKey> WithPageEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.PageEndpointOptions.Path = endpoint;

        return new FluxRestSetEndpointBuilder<TModel, TKey>(builder, (FluxRestSetEndpointOptions<TModel, TKey>)builder.SetOptions.PageEndpointOptions);
    }
}
