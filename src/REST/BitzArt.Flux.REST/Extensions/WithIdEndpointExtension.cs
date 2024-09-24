namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring the Id endpoint in <see cref="IFluxRestSetBuilder{TModel}"/><br/>
/// The Id endpoint is an endpoint used when fetching an entity by its Id.<br/>
/// Example: /api/entity/1
/// </summary>
public static class WithIdEndpointExtension
{
    /// <summary>
    /// Configures Id endpoint for the <see cref="IFluxRestSetBuilder{TModel}"/>.<br/>
    /// The Id endpoint is an endpoint used when fetching an entity by its Id.<br/>
    /// Example: /api/entity/1
    /// </summary>
    /// <returns>
    /// The <see cref="IFluxRestSetBuilder{TModel}"/> with Id endpoint configured
    /// </returns>
    public static IFluxRestSetBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    /// <summary>
    /// Configures Id endpoint for the <see cref="IFluxRestSetBuilder{TModel,TKey}"/>.<br/>
    /// The Id endpoint is an endpoint used when fetching an entity by its Id.<br/>
    /// Example: /api/entity/1
    /// </summary>
    /// <returns>
    /// The <see cref="IFluxRestSetBuilder{TModel,TKey}"/> with Id endpoint configured
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel}(IFluxRestSetBuilder{TModel},string)"/>
    public static IFluxRestSetBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, Func<string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel}(IFluxRestSetBuilder{TModel},string)"/>
    public static IFluxRestSetBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, Func<object?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel}(IFluxRestSetBuilder{TModel},string)"/>
    public static IFluxRestSetBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, Func<object?, object[]?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey?, object[]?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }
}
