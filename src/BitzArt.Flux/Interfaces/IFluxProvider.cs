namespace BitzArt.Flux;

internal interface IFluxProvider
{
    internal ICollection<IFluxServiceProvider> ServiceContexts { get; }

    internal IFluxServiceProvider GetServiceProvider(string name);

    internal IFluxEntityContext<TEntity> GetEntityContext<TEntity>(IServiceProvider services, string? serviceName = null)
        where TEntity : class;

    internal IFluxEntityContext<TEntity, TKey> GetEntityContext<TEntity, TKey>(IServiceProvider services, string? serviceName = null)
        where TEntity : class;
}