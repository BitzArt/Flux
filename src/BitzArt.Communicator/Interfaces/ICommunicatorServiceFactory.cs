namespace BitzArt.Communicator;

internal interface ICommunicatorServiceFactory
{
    internal ICollection<ICommunicatorServiceProvider> Providers { get; }

    internal ICommunicationContext<TEntity> GetEntityCommunicator<TEntity>(IServiceProvider services, object? options)
        where TEntity : class;

    internal ICommunicationContext<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, object? options)
        where TEntity : class;
}