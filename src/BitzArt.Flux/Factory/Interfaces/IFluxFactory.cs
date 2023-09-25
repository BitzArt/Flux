namespace BitzArt.Flux;

internal interface IFluxFactory
{
    internal ICollection<IFluxServiceFactory> ServiceContexts { get; }

    internal IFluxServiceFactory GetServiceProvider(string name);

    internal IFluxEntityContext<TEntity> GetEntityContext<TEntity>(IServiceProvider services, string? serviceName = null)
        where TEntity : class;

    internal IFluxEntityContext<TEntity, TKey> GetEntityContext<TEntity, TKey>(IServiceProvider services, string? serviceName = null)
        where TEntity : class;
}