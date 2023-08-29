using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

internal class CommunicationContext : ICommunicationContext
{
    private readonly IServiceProvider _serviceProvider;

    public CommunicationContext(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEntityContext<TEntity, TKey> Entity<TEntity, TKey>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<IEntityContext<TEntity, TKey>>();

    public IEntityContext<TEntity> Entity<TEntity>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<IEntityContext<TEntity>>();
}
