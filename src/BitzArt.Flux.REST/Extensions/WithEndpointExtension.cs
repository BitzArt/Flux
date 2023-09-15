namespace BitzArt.Flux;

public static class WithEndpointExtension
{
    public static IFluxRestEntityBuilder<TEntity> WithEndpoint<TEntity>(this IFluxRestEntityBuilder<TEntity> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.Endpoint = endpoint;

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity, TKey> WithEndpoint<TEntity, TKey>(this IFluxRestEntityBuilder<TEntity, TKey> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.Endpoint = endpoint;

        return builder;
    }
}
