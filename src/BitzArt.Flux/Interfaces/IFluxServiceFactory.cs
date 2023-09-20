namespace BitzArt.Flux;

internal interface IFluxServiceFactory
{
    internal ICollection<IFluxServiceContext> ServiceContexts { get; }

    internal IFluxEntityContext<TEntity> GetEntityContext<TEntity>(IServiceProvider services, string? serviceName = null)
        where TEntity : class;

    internal IFluxEntityContext<TEntity, TKey> GetEntityContext<TEntity, TKey>(IServiceProvider services, string? serviceName = null)
        where TEntity : class;
}