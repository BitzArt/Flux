namespace BitzArt.Flux;

/// <summary>
/// Flux REST set endpoint builder.
/// </summary>
/// <typeparam name="TModel">
/// The model type of the set.
/// </typeparam>
public interface IFluxRestSetEndpointBuilder<TModel> : IFluxRestSetBuilder<TModel>
    where TModel : class
{
}

/// <inheritdoc cref="IFluxRestSetEndpointBuilder{TModel}"/>
/// /// <typeparam name="TModel">
/// The model type of the set.
/// </typeparam>
/// <typeparam name="TKey">
/// The key type of the set.
/// </typeparam>
public interface IFluxRestSetEndpointBuilder<TModel, TKey> : IFluxRestSetEndpointBuilder<TModel>
    where TModel : class
{
}
