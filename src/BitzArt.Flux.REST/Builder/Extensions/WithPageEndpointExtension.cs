namespace BitzArt.Flux;

public static class WithPageEndpointExtension
{
    public static IFluxRestModelBuilder<TModel> WithPageEndpoint<TModel>(this IFluxRestModelBuilder<TModel> builder, string endpoint)
        where TModel : class
    {
        builder.ModelOptions.PageEndpoint = endpoint;

        return builder;
    }

    public static IFluxRestModelBuilder<TModel, TKey> WithPageEndpoint<TModel, TKey>(this IFluxRestModelBuilder<TModel, TKey> builder, string endpoint)
        where TModel : class
    {
        builder.ModelOptions.PageEndpoint = endpoint;

        return builder;
    }
}
