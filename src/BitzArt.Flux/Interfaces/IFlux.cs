namespace BitzArt.Flux;

public interface IFlux
{
    IFluxEntityContext<TEntity, TKey> Entity<TEntity, TKey>() where TEntity : class;
    IFluxEntityContext<TEntity> Entity<TEntity>() where TEntity : class;
}