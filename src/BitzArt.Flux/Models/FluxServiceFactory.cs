namespace BitzArt.Flux;

internal class FluxServiceFactory : IFluxServiceFactory
{
    public ICollection<IFluxServiceProvider> Providers { get; private set; }

    public FluxServiceFactory()
    {
        Providers = new HashSet<IFluxServiceProvider>();
    }

    public IFluxEntityContext<TEntity> GetEntityContext<TEntity>(
        IServiceProvider services,
        object? options)
        where TEntity : class
    {
        var provider = Providers
            .AsQueryable()
            //.WhereIf(serviceName is not null, x => x.ServiceName == serviceName)
            .Where(x => x.ContainsSignature(new (typeof(TEntity), null)))
            .FirstOrDefault();

        if (provider is null) throw new Exception("Flux Service Provider not found.");

        return provider.GetEntityContext<TEntity>(services, options);
    }

    public IFluxEntityContext<TEntity, TKey> GetEntityContext<TEntity, TKey>(
        IServiceProvider services,
        object? options)
        where TEntity : class
    {
        var provider = Providers
            .AsQueryable()
            //.WhereIf(serviceName is not null, x => x.ServiceName == serviceName)
            .Where(x => x.ContainsSignature(new(typeof(TEntity), typeof(TKey))))
            .FirstOrDefault();

        if (provider is null) throw new Exception("Flux Service Provider not found.");

        return provider.GetEntityContext<TEntity, TKey>(services, options);
    }
}