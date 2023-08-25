namespace BitzArt.Communicator;

internal class CommunicatorServiceFactory : ICommunicatorServiceFactory
{
    public ICollection<ICommunicatorServiceProvider> Providers { get; private set; }

    public CommunicatorServiceFactory()
    {
        Providers = new HashSet<ICommunicatorServiceProvider>();
    }

    public IEntityCommunicator<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(IServiceProvider services, string endpoint, string? serviceName = null) where TEntity : class
    {
        var provider = Providers
            .AsQueryable()
            .WhereIf(serviceName is not null, x => x.ServiceName == serviceName)
            .Where(x => x.ContainsSignature(new(typeof(TEntity), typeof(TKey))))
            .FirstOrDefault();

        if (provider is null) throw new Exception("Communicator Provider not found.");

        return provider.GetEntityCommunicator<TEntity, TKey>(services, endpoint);
    }
}