namespace BitzArt.Flux;

public static class WithIdEndpointExtension
{
    public static IFluxRestSetBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    public static IFluxRestSetBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, Func<string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    public static IFluxRestSetBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, Func<object?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    public static IFluxRestSetBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, Func<object?, object[]?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey?, object[]?, string> getEndpoint)
        where TModel : class
    {
        builder.SetOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }
}
