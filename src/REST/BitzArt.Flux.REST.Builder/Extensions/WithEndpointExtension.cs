using BitzArt.Flux.REST;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring the default endpoint of a set.
/// </summary>
public static class WithEndpointExtension
{
    /// <summary>
    /// Configures the default endpoint for the <see cref="IFluxRestSetBuilder{TModel, TKey}"/>.
    /// </summary>
    /// <returns>
    /// The <see cref="IFluxRestSetBuilder{TModel, TKey}"/> with the endpoint configured.
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint)
        where TModel : class
        => builder.WithEndpoint<TModel, TKey, RestRequestParameters?>(endpoint);

    /// <summary>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TParameters>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint)
        where TModel : class
        where TParameters : IRequestParameters?
    {
        var options = new FluxRestSetEndpointOptions<TModel, TKey, TParameters>(builder.SetOptions, endpoint);
        builder.SetOptions.EndpointCollection.Add(options);

        return builder;
    }

    /// <summary>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint,
        Func<RequestParameters?, RestRequestParameters> transformParameters)
        where TModel : class
        => builder.WithEndpoint<TModel, TKey, RequestParameters?, RestRequestParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TInputParameters>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint,
        Func<TInputParameters?, RestRequestParameters> transformParameters)
        where TModel : class
        where TInputParameters : IRequestParameters?
        => builder.WithEndpoint<TModel, TKey, TInputParameters?, RestRequestParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TOutputParameters>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint,
        Func<RequestParameters?, TOutputParameters> transformParameters)
        where TModel : class
        where TOutputParameters : IRestRequestParameters
        => builder.WithEndpoint<TModel, TKey, RequestParameters?, TOutputParameters>(endpoint, transformParameters);

    /// <summary>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </summary>
    /// <returns>
    /// <inheritdoc cref="WithEndpoint{TModel, TKey}(IFluxRestSetBuilder{TModel, TKey}, string)"/>
    /// </returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TInputParameters, TOutputParameters>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint,
        Func<TInputParameters?, TOutputParameters> transformParameters)
        where TModel : class
        where TInputParameters : IRequestParameters?
        where TOutputParameters : IRestRequestParameters
    {
        var options = new FluxRestSetEndpointOptions<TModel, TKey, TInputParameters>(builder.SetOptions, endpoint, (parameters) => transformParameters(parameters));
        builder.SetOptions.EndpointCollection.Add(options);

        return builder;
    }
}
