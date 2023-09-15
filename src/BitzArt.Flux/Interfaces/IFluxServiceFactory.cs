namespace BitzArt.Flux;

internal interface IFluxServiceFactory
{
    internal ICollection<IFluxServiceProvider> Providers { get; }

    internal IFluxEntityContext<TEntity> GetEntityContext<TEntity>(IServiceProvider services, object? options)
        where TEntity : class;

    internal IFluxEntityContext<TEntity, TKey> GetEntityContext<TEntity, TKey>(IServiceProvider services, object? options)
        where TEntity : class;
}