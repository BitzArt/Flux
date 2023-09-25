namespace BitzArt.Flux;

public interface IFluxContext
{
    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>() where TEntity : class;
    public IFluxEntityContext<TEntity> Entity<TEntity>() where TEntity : class;
    IFluxServiceContext Service(string serviceName);
}