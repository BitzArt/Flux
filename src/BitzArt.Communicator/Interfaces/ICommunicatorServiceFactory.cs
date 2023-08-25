namespace BitzArt.Communicator;

public interface ICommunicatorServiceFactory
{
    public ICollection<ICommunicatorServiceProvider> Providers { get; }

    public IEntityCommunicator<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, string endpoint, string? serviceName = null)
        where TEntity : class;
}