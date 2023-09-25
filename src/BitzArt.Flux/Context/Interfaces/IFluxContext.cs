namespace BitzArt.Flux;

public interface IFluxContext
{
    public IFluxServiceContext Service(string serviceName);

    public IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>(string? serviceName = null) where TEntity : class;
    public IFluxEntityContext<TEntity> Entity<TEntity>(string? serviceName = null) where TEntity : class;
}