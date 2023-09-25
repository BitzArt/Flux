namespace BitzArt.Flux;

internal class FluxServiceContext : IFluxServiceContext
{
    internal readonly IFluxServiceProvider Provider;
    private readonly IServiceProvider _serviceProvider;

    public FluxServiceContext(IFluxServiceProvider provider, IServiceProvider serviceProvider)
    {
        Provider = provider;
        _serviceProvider = serviceProvider;
    }

    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>()
        where TEntity : class
        => Provider.CreateEntityContext<TEntity, TKey>(_serviceProvider);

    public IFluxEntityContext<TEntity> Entity<TEntity>()
        where TEntity : class
        => Provider.CreateEntityContext<TEntity>(_serviceProvider);
}
