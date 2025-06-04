namespace BitzArt.Flux;

/// <summary>
/// Flux Set Builder.
/// </summary>
/// <typeparam name="TModel">Set model type.</typeparam>
/// <typeparam name="TKey">Set key type.</typeparam>
public interface IFluxSetBuilder<TModel, TKey> : IFluxSetBuilder
    where TModel : class
{
}

/// <inheritdoc cref="IFluxSetBuilder{TModel, TKey}"/>
public interface IFluxSetBuilder : IFluxServiceBuilder
{
    /// <summary>
    /// Service builder instance that was used to create this set builder.
    /// </summary>
    public IFluxServiceBuilder ServiceBuilder { get; }

    // ================ Inherited members ================

    IFluxBuilder IFluxServiceBuilder.FluxBuilder => ServiceBuilder.FluxBuilder;
    string IFluxServiceBuilder.ServiceName => ServiceBuilder.ServiceName;
    string IFluxServiceBuilder.ImplementationName => ServiceBuilder.ImplementationName;
}