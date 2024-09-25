using BitzArt.Flux.REST;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring the Id endpoint in <see cref="IFluxRestSetBuilder{TModel, TKey}"/><br/>
/// The Id endpoint is an endpoint used when fetching an entity by its Id.<br/>
/// Example: /api/entity/1
/// </summary>
public static class WithIdEndpointExtension
{
    /// <summary>
    /// Configures Id endpoint for the <see cref="IFluxRestSetBuilder{TModel,TKey}"/>.<br/>
    /// The Id endpoint is an endpoint used when fetching an entity by its Id.<br/>
    /// Example: /api/entity/1
    /// </summary>
    /// <returns>
    /// The <see cref="IFluxRestSetIdEndpointBuilder{TModel,TKey}"/> with Id endpoint configured
    /// </returns>
    public static IFluxRestSetIdEndpointBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.IdEndpointOptions.GetPathFunc = (key, parameters) => endpoint;

        return new FluxRestSetIdEndpointBuilder<TModel, TKey>(builder, (FluxRestSetIdEndpointOptions<TModel, TKey>)builder.SetOptions.IdEndpointOptions);
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string)"/>
    public static IFluxRestSetIdEndpointBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.IdEndpointOptions.GetPathFunc = (key, parameters) => getEndpoint();

        return new FluxRestSetIdEndpointBuilder<TModel, TKey>(builder, (FluxRestSetIdEndpointOptions<TModel, TKey>)builder.SetOptions.IdEndpointOptions);
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string)"/>
    public static IFluxRestSetIdEndpointBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.IdEndpointOptions.GetPathFunc = (key, parameters) => getEndpoint((TKey?)key);

        return new FluxRestSetIdEndpointBuilder<TModel, TKey>(builder, (FluxRestSetIdEndpointOptions<TModel, TKey>)builder.SetOptions.IdEndpointOptions);
    }

    /// <inheritdoc cref="WithIdEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string)"/>
    public static IFluxRestSetIdEndpointBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey?, object[]?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.IdEndpointOptions.GetPathFunc = (key, parameters) => getEndpoint((TKey?)key, parameters);

        return new FluxRestSetIdEndpointBuilder<TModel, TKey>(builder, (FluxRestSetIdEndpointOptions<TModel, TKey>)builder.SetOptions.IdEndpointOptions);
    }
}
