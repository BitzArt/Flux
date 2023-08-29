namespace BitzArt.Communicator;

public interface ICommunicationContext
{
    IEntityContext<TEntity, TKey> Entity<TEntity, TKey>() where TEntity : class;
    IEntityContext<TEntity> Entity<TEntity>() where TEntity : class;
}