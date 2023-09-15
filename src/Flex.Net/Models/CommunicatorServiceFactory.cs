namespace Flex;

internal class CommunicatorServiceFactory : ICommunicatorServiceFactory
{
    public ICollection<ICommunicatorServiceProvider> Providers { get; private set; }

    public CommunicatorServiceFactory()
    {
        Providers = new HashSet<ICommunicatorServiceProvider>();
    }

    public ICommunicationContext<TEntity> GetEntityCommunicator<TEntity>(
        IServiceProvider services,
        object? options)
        where TEntity : class
    {
        var provider = Providers
            .AsQueryable()
            //.WhereIf(serviceName is not null, x => x.ServiceName == serviceName)
            .Where(x => x.ContainsSignature(new (typeof(TEntity), null)))
            .FirstOrDefault();

        if (provider is null) throw new Exception("Communicator Provider not found.");

        return provider.GetEntityCommunicator<TEntity>(services, options);
    }

    public ICommunicationContext<TEntity, TKey> GetEntityCommunicator<TEntity, TKey>(
        IServiceProvider services,
        object? options)
        where TEntity : class
    {
        var provider = Providers
            .AsQueryable()
            //.WhereIf(serviceName is not null, x => x.ServiceName == serviceName)
            .Where(x => x.ContainsSignature(new(typeof(TEntity), typeof(TKey))))
            .FirstOrDefault();

        if (provider is null) throw new Exception("Communicator Provider not found.");

        return provider.GetEntityCommunicator<TEntity, TKey>(services, options);
    }
}