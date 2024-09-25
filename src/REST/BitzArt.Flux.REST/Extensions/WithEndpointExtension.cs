namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring default set endpoint in <see cref="IFluxRestSetBuilder{TModel}"/> and <see cref="IFluxRestSetBuilder{TModel, TKey}"/>
/// </summary>
public static class WithEndpointExtension
{
    /// <summary>
    /// Configures a default endpoint for the <see cref="IFluxRestSetBuilder{TModel}"/>
    /// </summary>
    /// /// <returns>
    /// The <see cref="IFluxRestSetBuilder{TModel}"/> with the endpoint configured
    /// </returns>
    public static IFluxRestSetBuilder<TModel> WithEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.EndpointOptions.Path = endpoint;

        return builder;
    }

    /// <summary>
    /// Configures a default endpoint for the <see cref="IFluxRestSetBuilder{TModel, TKey}"/>
    /// </summary>
    /// <returns>
    /// The <see cref="IFluxRestSetBuilder{TModel, TKey}"/> with the endpoint configured
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.EndpointOptions.Path = endpoint;

        return builder;
    }
}
