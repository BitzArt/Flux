namespace BitzArt.Flux;

public static class WithEndpointExtension
{
    public static IFluxRestSetBuilder<TModel> WithEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.Endpoint = endpoint;

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.Endpoint = endpoint;

        return builder;
    }
}
