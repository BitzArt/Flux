using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class Flux : IFlux
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IFluxServiceFactory _serviceFactory;

    public Flux(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _serviceFactory = serviceProvider.GetRequiredService<IFluxServiceFactory>();
    }

    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<IFluxEntityContext<TEntity, TKey>>();

    public IFluxEntityContext<TEntity> Entity<TEntity>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<IFluxEntityContext<TEntity>>();
}
