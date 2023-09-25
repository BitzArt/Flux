namespace BitzArt.Flux;

internal class FluxContext : IFluxContext
{
    private readonly IFluxFactory _factory;
    private readonly IServiceProvider _serviceProvider;

    public FluxContext(IFluxFactory factory, IServiceProvider serviceProvider)
    {
        _factory = factory;
        _serviceProvider = serviceProvider;
    }

    public IFluxServiceContext Service(string serviceName)
        => new FluxServiceContext(_factory.GetServiceProvider(serviceName), _serviceProvider);

    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>(string? serviceName = null)
        where TEntity : class
        => _factory.GetEntityContext<TEntity, TKey>(_serviceProvider, serviceName);

    public IFluxEntityContext<TEntity> Entity<TEntity>(string? serviceName = null)
        where TEntity : class
        => _factory.GetEntityContext<TEntity>(_serviceProvider, serviceName);
}
