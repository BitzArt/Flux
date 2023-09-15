namespace Flex;

internal interface ICommunicatorServiceProvider
{
    internal string ServiceName { get; }

    internal void AddSignature(CommunicatorEntitySignature entitySignature);
    internal bool ContainsSignature(CommunicatorEntitySignature entitySignature);

    ICommunicationContext<TEntity> GetEntityCommunicator<TEntity>(IServiceProvider services, object? options)
        where TEntity : class;

    ICommunicationContext<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, object? options)
        where TEntity : class;
}