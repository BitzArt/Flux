namespace BitzArt.Communicator;

public interface ICommunicatorServiceProvider
{
    public string ServiceName { get; }

    public void AddSignature(CommunicatorEntitySignature entitySignature);
    public bool ContainsSignature(CommunicatorEntitySignature entitySignature);

    IEntityContext<TEntity> GetEntityCommunicator<TEntity>(IServiceProvider services, string? endpoint)
        where TEntity : class;

    IEntityContext<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, string? endpoint)
        where TEntity : class;
}