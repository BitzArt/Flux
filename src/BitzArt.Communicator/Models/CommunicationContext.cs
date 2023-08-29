using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

internal class CommunicationContext : ICommunicationContext
{
    private readonly IServiceProvider _serviceProvider;

    public CommunicationContext(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommunicationContext<TEntity, TKey> Entity<TEntity, TKey>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<ICommunicationContext<TEntity, TKey>>();

    public ICommunicationContext<TEntity> Entity<TEntity>()
        where TEntity : class
        => _serviceProvider.GetRequiredService<ICommunicationContext<TEntity>>();
}
