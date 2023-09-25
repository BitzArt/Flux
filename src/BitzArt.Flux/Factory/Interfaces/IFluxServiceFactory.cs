namespace BitzArt.Flux;

public interface IFluxServiceFactory
{
    internal string ServiceName { get; }

    internal IFluxEntityContext<TEntity> CreateEntityContext<TEntity>(IServiceProvider services) where TEntity : class;
    internal IFluxEntityContext<TEntity, TKey> CreateEntityContext<TEntity, TKey>(IServiceProvider services) where TEntity : class;
    internal void AddEntity<TEntity>(object options) where TEntity : class;
    internal void AddEntity<TEntity, TKey>(object options) where TEntity : class;

    internal bool ContainsSignature<TEntity>();
    internal bool ContainsSignature<TEntity, TKey>();
}