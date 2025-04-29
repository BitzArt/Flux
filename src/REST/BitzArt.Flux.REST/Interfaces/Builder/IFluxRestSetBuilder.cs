namespace BitzArt.Flux.REST;

/// <summary>
/// Flux REST set builder.
/// </summary>
/// <typeparam name="TModel">Set model type.</typeparam>
/// <typeparam name="TKey">Set key type.</typeparam>
public interface IFluxRestSetBuilder<TModel, TKey> : IFluxRestServiceBuilder, IFluxSetBuilder
    where TModel : class
{
    internal new IFluxRestServiceBuilder ServiceBuilder { get; }

    // ================ Inherited members ================

    IFluxServiceBuilder IFluxSetBuilder.ServiceBuilder => ServiceBuilder;
    FluxRestServiceOptions IFluxRestServiceBuilder.ServiceOptions => ServiceBuilder.ServiceOptions;
}
