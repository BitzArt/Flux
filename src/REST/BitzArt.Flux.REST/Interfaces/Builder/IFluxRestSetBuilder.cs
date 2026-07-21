namespace BitzArt.Flux.Rest;

/// <summary>
/// Flux REST set builder.
/// </summary>
/// <typeparam name="TModel">Set model type.</typeparam>
/// <typeparam name="TKey">Set key type.</typeparam>
public interface IFluxRestSetBuilder<TModel, TKey> : IFluxRestServiceBuilder, IFluxSetBuilder<TModel, TKey>
    where TModel : class
{
    internal new IFluxRestServiceBuilder ServiceBuilder { get; }

    internal FluxRestSetConfiguration SetConfiguration { get; }

    // ================ Inherited members ================

    IFluxServiceBuilder IFluxSetBuilder.ServiceBuilder => ServiceBuilder;
    FluxRestServiceConfiguration IFluxRestServiceBuilder.ServiceConfiguration => ServiceBuilder.ServiceConfiguration;
}
