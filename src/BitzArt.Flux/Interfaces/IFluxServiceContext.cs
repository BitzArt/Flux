namespace BitzArt.Flux;

public interface IFluxServiceContext
{
    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>() where TEntity : class;
    public IFluxEntityContext<TEntity> Entity<TEntity>() where TEntity : class;
}