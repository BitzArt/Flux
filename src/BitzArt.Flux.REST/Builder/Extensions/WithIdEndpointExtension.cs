namespace BitzArt.Flux;

public static class WithIdEndpointExtension
{
    public static IFluxRestEntityBuilder<TEntity> WithIdEndpoint<TEntity>(this IFluxRestEntityBuilder<TEntity> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity, TKey> WithIdEndpoint<TEntity, TKey>(this IFluxRestEntityBuilder<TEntity, TKey> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity> WithIdEndpoint<TEntity>(this IFluxRestEntityBuilder<TEntity> builder, Func<string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity, TKey> WithIdEndpoint<TEntity, TKey>(this IFluxRestEntityBuilder<TEntity, TKey> builder, Func<string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity> WithIdEndpoint<TEntity>(this IFluxRestEntityBuilder<TEntity> builder, Func<object?, string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity, TKey> WithIdEndpoint<TEntity, TKey>(this IFluxRestEntityBuilder<TEntity, TKey> builder, Func<TKey?, string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity> WithIdEndpoint<TEntity>(this IFluxRestEntityBuilder<TEntity> builder, Func<object?, object[]?, string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }

    public static IFluxRestEntityBuilder<TEntity, TKey> WithIdEndpoint<TEntity, TKey>(this IFluxRestEntityBuilder<TEntity, TKey> builder, Func<TKey?, object[]?, string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }
}
