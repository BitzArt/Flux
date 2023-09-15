namespace Flex;

public static class WithIdEndpointExtension
{
    public static ICommunicatorRestEntityBuilder<TEntity> WithIdEndpoint<TEntity>(this ICommunicatorRestEntityBuilder<TEntity> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    public static ICommunicatorRestEntityBuilder<TEntity, TKey> WithIdEndpoint<TEntity, TKey>(this ICommunicatorRestEntityBuilder<TEntity, TKey> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => endpoint;

        return builder;
    }

    public static ICommunicatorRestEntityBuilder<TEntity> WithIdEndpoint<TEntity>(this ICommunicatorRestEntityBuilder<TEntity> builder, Func<string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    public static ICommunicatorRestEntityBuilder<TEntity, TKey> WithIdEndpoint<TEntity, TKey>(this ICommunicatorRestEntityBuilder<TEntity, TKey> builder, Func<string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => getEndpoint();

        return builder;
    }

    public static ICommunicatorRestEntityBuilder<TEntity> WithIdEndpoint<TEntity>(this ICommunicatorRestEntityBuilder<TEntity> builder, Func<object?, string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    public static ICommunicatorRestEntityBuilder<TEntity, TKey> WithIdEndpoint<TEntity, TKey>(this ICommunicatorRestEntityBuilder<TEntity, TKey> builder, Func<TKey?, string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = (key, parameters) => getEndpoint(key);

        return builder;
    }

    public static ICommunicatorRestEntityBuilder<TEntity> WithIdEndpoint<TEntity>(this ICommunicatorRestEntityBuilder<TEntity> builder, Func<object?, object[]?, string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }

    public static ICommunicatorRestEntityBuilder<TEntity, TKey> WithIdEndpoint<TEntity, TKey>(this ICommunicatorRestEntityBuilder<TEntity, TKey> builder, Func<TKey?, object[]?, string> getEndpoint)
        where TEntity : class
    {
        builder.EntityOptions.GetIdEndpointAction = getEndpoint;

        return builder;
    }
}
