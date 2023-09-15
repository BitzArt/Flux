namespace BitzArt.Flux;

internal interface IFluxServiceProvider
{
    internal string ServiceName { get; }

    internal void AddSignature(FluxEntitySignature entitySignature);
    internal bool ContainsSignature(FluxEntitySignature entitySignature);

    IFluxEntityContext<TEntity> GetEntityContext<TEntity>(IServiceProvider services, object? options)
        where TEntity : class;

    IFluxEntityContext<TEntity, TKey> GetEntityContext<TEntity, TKey>(IServiceProvider services, object? options)
        where TEntity : class;
}