namespace BitzArt.Communicator;

public static class WithEndpointExtension
{
    public static ICommunicatorRestEntityBuilder<TEntity> WithEndpoint<TEntity>(this ICommunicatorRestEntityBuilder<TEntity> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.Endpoint = endpoint;

        return builder;
    }

    public static ICommunicatorRestEntityBuilder<TEntity, TKey> WithEndpoint<TEntity, TKey>(this ICommunicatorRestEntityBuilder<TEntity, TKey> builder, string endpoint)
        where TEntity : class
    {
        builder.EntityOptions.Endpoint = endpoint;

        return builder;
    }
}
