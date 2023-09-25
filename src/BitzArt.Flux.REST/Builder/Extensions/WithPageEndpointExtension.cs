namespace BitzArt.Flux;

public static class WithPageEndpointExtension
{
    public static IFluxRestEntityBuilder<TEntity> WithPageEndpoint<TEntity>(this IFluxRestEntityBuilder<TEntity> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.PageEndpoint = endpoint;

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity, TKey> WithPageEndpoint<TEntity, TKey>(this IFluxRestEntityBuilder<TEntity, TKey> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.PageEndpoint = endpoint;

        return builder;
    }
}
