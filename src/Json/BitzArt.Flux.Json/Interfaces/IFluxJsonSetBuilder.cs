namespace BitzArt.Flux;

public interface IFluxJsonSetBuilder<TModel> : IFluxJsonServiceBuilder
    where TModel : class
{
    internal FluxJsonSetOptions<TModel> SetOptions { get; }
}

public interface IFluxJsonSetBuilder<TModel, TKey> : IFluxJsonSetBuilder<TModel>
    where TModel : class
{
    internal new FluxJsonSetOptions<TModel, TKey> SetOptions { get; }
}