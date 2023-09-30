namespace BitzArt.Flux;

public static class WithIdEndpointExtension
{
    public static IFluxRestModelBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestModelBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.ModelOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    public static IFluxRestModelBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestModelBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.ModelOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    public static IFluxRestModelBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestModelBuilder<TModel> builder, Func<string> getEndpoint)
        where TModel : class
    {
        builder.ModelOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    public static IFluxRestModelBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestModelBuilder<TModel, TKey> builder, Func<string> getEndpoint)
        where TModel : class
    {
        builder.ModelOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    public static IFluxRestModelBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestModelBuilder<TModel> builder, Func<object?, string> getEndpoint)
        where TModel : class
    {
        builder.ModelOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    public static IFluxRestModelBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestModelBuilder<TModel, TKey> builder, Func<TKey?, string> getEndpoint)
        where TModel : class
    {
        builder.ModelOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    public static IFluxRestModelBuilder<TModel> WithIdEndpoint<TModel>(this IFluxRestModelBuilder<TModel> builder, Func<object?, object[]?, string> getEndpoint)
        where TModel : class
    {
        builder.ModelOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }

    public static IFluxRestModelBuilder<TModel, TKey> WithIdEndpoint<TModel, TKey>(this IFluxRestModelBuilder<TModel, TKey> builder, Func<TKey?, object[]?, string> getEndpoint)
        where TModel : class
    {
        builder.ModelOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }
}
