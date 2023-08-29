namespace BitzArt.Communicator;

public interface ICommunicatorServiceFactory
{
    public ICollection<ICommunicatorServiceProvider> Providers { get; }

    public IEntityContext<TEntity> GetEntityCommunicator<TEntity>(IServiceProvider services, string? endpoint, string? serviceName = null)
        where TEntity : class;

    public IEntityContext<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, string? endpoint, string? serviceName = null)
        where TEntity : class;
}