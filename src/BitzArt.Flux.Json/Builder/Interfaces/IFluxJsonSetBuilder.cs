namespace BitzArt.Flux;

public interface IFluxJsonSetBuilder<TModel> : IFluxModelBuilder
    where TModel : class
{
    public FluxJsonSetOptions<TModel> SetOptions { get; }
}

public interface IFluxJsonSetBuilder<TModel, TKey> : IFluxJsonSetBuilder<TModel>
    where TModel : class
{
    public new FluxJsonSetOptions<TModel, TKey> SetOptions { get; }
}