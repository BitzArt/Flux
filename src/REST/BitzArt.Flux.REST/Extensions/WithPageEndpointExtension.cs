namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring the page endpoint of a set.
/// </summary>
public static class WithPageEndpointExtension
{
    /// <summary>
    /// Configures the page endpoint for the <see cref="IFluxRestSetBuilder{TModel}"/><br/>
    /// The page endpoint is an endpoint used when fetching a page of entities.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type of the set.
    /// </typeparam>
    /// <returns>
    /// The <see cref="IFluxRestSetBuilder{TModel}"/> with the page endpoint configured
    /// </returns>
    public static IFluxRestSetBuilder<TModel> WithPageEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.PageEndpoint = endpoint;

        return builder;
    }

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
    /// The <see cref="IFluxRestSetBuilder{TModel,TKey}"/> with the page endpoint configured
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithPageEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.PageEndpoint = endpoint;

        return builder;
    }
}
