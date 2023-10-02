namespace BitzArt.Flux;

public static class WithPageEndpointExtension
{
    public static IFluxRestSetBuilder<TModel> WithPageEndpoint<TModel>(this IFluxRestSetBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.PageEndpoint = endpoint;

        return builder;
    }

    public static IFluxRestSetBuilder<TModel, TKey> WithPageEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.SetOptions.PageEndpoint = endpoint;

        return builder;
    }
}
