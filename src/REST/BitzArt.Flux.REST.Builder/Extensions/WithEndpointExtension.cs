using BitzArt.Flux.REST;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring the default endpoint of a set.
/// </summary>
public static class WithEndpointExtension
{
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint)
        where TModel : class
        => builder.WithEndpoint<TModel, TKey, RestRequestParameters>(endpoint);

    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TParameters>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint)
        where TModel : class
        where TParameters : notnull, IFluxOperationParameters
    {
        var options = new FluxRestSetEndpointOptions<TModel, TKey, TParameters>(builder.SetOptions, endpoint);
        builder.SetOptions.EndpointCollection.Add(options);

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint,
        Func<RequestParameters?, RestRequestParameters> transformParameters)
        where TModel : class
        => builder.WithEndpoint<TModel, TKey, RequestParameters?, RestRequestParameters>(endpoint, transformParameters);

    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TInputParameters>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint,
        Func<TInputParameters?, RestRequestParameters> transformParameters)
        where TModel : class
        where TInputParameters : IFluxOperationParameters?
        => builder.WithEndpoint<TModel, TKey, TInputParameters?, RestRequestParameters>(endpoint, transformParameters);

    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TOutputParameters>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint,
        Func<RequestParameters?, TOutputParameters> transformParameters)
        where TModel : class
        where TOutputParameters : IFluxRestOperationParameters
        => builder.WithEndpoint<TModel, TKey, RequestParameters?, TOutputParameters>(endpoint, transformParameters);

    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TInputParameters, TOutputParameters>(
        this IFluxRestSetBuilder<TModel, TKey> builder,
        string endpoint,
        Func<TInputParameters?, TOutputParameters> transformParameters)
        where TModel : class
        where TInputParameters : IFluxOperationParameters?
        where TOutputParameters : IFluxRestOperationParameters
    {
        var options = new FluxRestSetEndpointOptions<TModel, TKey, TInputParameters>(builder.SetOptions, endpoint, (parameters) => transformParameters(parameters));
        builder.SetOptions.EndpointCollection.Add(options);

        return builder;
    }
}
