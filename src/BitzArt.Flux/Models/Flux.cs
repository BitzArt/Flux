using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class Flux : IFlux
{
    private readonly IServiceProvider _serviceProvider;

    public Flux(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<IFluxEntityContext<TEntity, TKey>>();

    public IFluxEntityContext<TEntity> Entity<TEntity>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<IFluxEntityContext<TEntity>>();
}
