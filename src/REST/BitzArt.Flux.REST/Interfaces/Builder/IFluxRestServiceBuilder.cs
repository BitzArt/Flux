namespace BitzArt.Flux.Rest;

/// <summary>
/// Flux REST service builder.
/// </summary>
public interface IFluxRestServiceBuilder : IFluxServiceBuilder
{
    internal FluxRestServiceConfiguration ServiceConfiguration { get; }
}
