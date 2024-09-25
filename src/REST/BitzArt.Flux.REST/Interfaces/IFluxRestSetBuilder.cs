using BitzArt.Flux.REST;

namespace BitzArt.Flux;

/// <summary>
/// Flux REST set builder.
/// </summary>
/// <typeparam name="TModel">
/// The model type of the set.
/// </typeparam>
public interface IFluxRestSetBuilder<TModel> : IFluxRestServiceBuilder
    where TModel : class
{
    internal IFluxRestSetOptions<TModel> SetOptions { get; }
}

/// <inheritdoc cref="IFluxRestSetBuilder{TModel}"/>"
/// <typeparam name="TModel">
/// The model type of the set.
/// </typeparam>
/// <typeparam name="TKey">
/// The key type of the set.
/// </typeparam>
public interface IFluxRestSetBuilder<TModel, TKey> : IFluxRestSetBuilder<TModel>
    where TModel : class
{
    internal new FluxRestSetOptions<TModel, TKey> SetOptions { get; }
}
