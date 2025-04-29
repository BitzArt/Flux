namespace BitzArt.Flux;

/// <summary>
/// Flux Set Builder.
/// </summary>
public interface IFluxSetBuilder : IFluxServiceBuilder
{
    /// <summary>
    /// Service builder instance that was used to create this set builder.
    /// </summary>
    public IFluxServiceBuilder ServiceBuilder { get; }

    // ================ Inherited members ================

    IFluxBuilder IFluxServiceBuilder.FluxBuilder => ServiceBuilder.FluxBuilder;
    string IFluxServiceBuilder.ServiceName => ServiceBuilder.ServiceName;
}