namespace BitzArt.Flux;

internal class FluxContext : IFluxContext
{
    private readonly IFluxProvider _provider;
    private readonly IServiceProvider _serviceProvider;

    public FluxContext(IFluxProvider provider, IServiceProvider serviceProvider)
    {
        _provider = provider;
        _serviceProvider = serviceProvider;
    }

    public IFluxServiceContext Service(string serviceName)
        => new FluxServiceContext(_provider.GetServiceProvider(serviceName), _serviceProvider);

    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>()
        where TEntity : class
        => _provider.GetEntityContext<TEntity, TKey>(_serviceProvider);

    public IFluxEntityContext<TEntity> Entity<TEntity>()
        where TEntity : class
        => _provider.GetEntityContext<TEntity>(_serviceProvider);
}
