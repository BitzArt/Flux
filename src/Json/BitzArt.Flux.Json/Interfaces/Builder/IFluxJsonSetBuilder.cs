namespace BitzArt.Flux.Json;

/// <summary>
/// Flux Json set builder.
/// </summary>
/// <typeparam name="TModel">Set model type.</typeparam>
public interface IFluxJsonSetBuilder<TModel> : IFluxJsonServiceBuilder, IFluxSetBuilder<TModel, object>
    where TModel : class
{
    internal new IFluxJsonServiceBuilder ServiceBuilder { get; }

    internal FluxJsonSetConfiguration<TModel> SetConfiguration { get; }

    // ================ Inherited members ================

    IFluxServiceBuilder IFluxSetBuilder.ServiceBuilder => ServiceBuilder;
    FluxJsonServiceConfiguration IFluxJsonServiceBuilder.ServiceConfiguration => ServiceBuilder.ServiceConfiguration;
}
