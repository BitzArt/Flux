namespace BitzArt.Flux.Json;

/// <summary>
/// Flux Json set builder.
/// </summary>
/// <typeparam name="TModel">Set model type.</typeparam>
/// <typeparam name="TKey">Set key type.</typeparam>
public interface IFluxJsonSetBuilder<TModel, TKey> : IFluxJsonServiceBuilder, IFluxSetBuilder<TModel, TKey>
    where TModel : class
{
    internal new IFluxJsonServiceBuilder ServiceBuilder { get; }

    internal FluxJsonSetConfiguration SetConfiguration { get; }

    // ================ Inherited members ================

    IFluxServiceBuilder IFluxSetBuilder.ServiceBuilder => ServiceBuilder;
    FluxJsonServiceConfiguration IFluxJsonServiceBuilder.ServiceConfiguration => ServiceBuilder.ServiceConfiguration;
}
