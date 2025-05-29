namespace BitzArt.Flux;

/// <inheritdoc/>
public interface IFluxJsonSetBuilder<TModel, TKey> : IFluxJsonServiceBuilder
    where TModel : class
{
    internal IFluxJsonSetOptions<TModel> SetOptions { get; }
}