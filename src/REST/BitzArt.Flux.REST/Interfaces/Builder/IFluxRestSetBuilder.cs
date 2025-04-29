namespace BitzArt.Flux.REST;

/// <summary>
/// Flux REST set builder.
/// </summary>
/// <typeparam name="TModel">Set model type.</typeparam>
/// <typeparam name="TKey">Set key type.</typeparam>
public interface IFluxRestSetBuilder<TModel, TKey> : IFluxRestServiceBuilder
    where TModel : class
{
    internal IFluxRestServiceBuilder ServiceBuilder { get; }

    // ================ Inherited members ================

    IFluxServiceBuilder IFluxRestServiceBuilder.SourceBuilder => ServiceBuilder.SourceBuilder;
    ServiceOptions IFluxRestServiceBuilder.ServiceOptions => ServiceBuilder.ServiceOptions;
}
