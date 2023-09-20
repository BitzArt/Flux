using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxServiceContext
{
    private readonly IServiceProvider _serviceProvider;
    private readonly string _serviceName;

    public FluxServiceContext(IServiceProvider serviceProvider, string serviceName)
    {
        _serviceProvider = serviceProvider;
        _serviceName = serviceName;
    }

    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<IFluxEntityContext<TEntity, TKey>>();

    public IFluxEntityContext<TEntity> Entity<TEntity>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<IFluxEntityContext<TEntity>>();
}
