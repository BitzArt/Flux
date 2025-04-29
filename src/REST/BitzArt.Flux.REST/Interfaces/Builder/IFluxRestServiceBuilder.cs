namespace BitzArt.Flux.REST;

/// <summary>
/// Flux REST service builder.
/// </summary>
public interface IFluxRestServiceBuilder : IFluxServiceBuilder
{
    internal IFluxServiceBuilder SourceBuilder { get; }

    internal FluxRestServiceOptions ServiceOptions { get; }

    // ================ Inherited members ================

    IFluxBuilder IFluxServiceBuilder.FluxBuilder => SourceBuilder.FluxBuilder;
    string IFluxServiceBuilder.ServiceName => SourceBuilder.ServiceName;
}
