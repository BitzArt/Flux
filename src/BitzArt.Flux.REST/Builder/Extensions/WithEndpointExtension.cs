namespace BitzArt.Flux;

public static class WithEndpointExtension
{
    public static IFluxRestModelBuilder<TModel> WithEndpoint<TModel>(this IFluxRestModelBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.ModelOptions.Endpoint = endpoint;

        return builder;
    }

    public static IFluxRestModelBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestModelBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.ModelOptions.Endpoint = endpoint;

        return builder;
    }
}
