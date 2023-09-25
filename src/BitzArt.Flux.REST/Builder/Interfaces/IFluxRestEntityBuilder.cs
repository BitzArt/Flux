namespace BitzArt.Flux;

public interface IFluxRestEntityBuilder<TEntity> : IFluxEntityBuilder, IFluxRestServiceBuilder
    where TEntity : class
{
    public FluxRestEntityOptions<TEntity> EntityOptions { get; }
}

public interface IFluxRestEntityBuilder<TEntity, TKey> : IFluxRestEntityBuilder<TEntity>
    where TEntity : class
{
    public new FluxRestEntityOptions<TEntity, TKey> EntityOptions { get; }
}
