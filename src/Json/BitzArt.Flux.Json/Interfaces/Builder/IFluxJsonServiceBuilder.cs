namespace BitzArt.Flux.Json;

/// <summary>
/// Flux Json service builder.
/// </summary>
public interface IFluxJsonServiceBuilder : IFluxServiceBuilder
{
    internal FluxJsonServiceConfiguration ServiceConfiguration { get; }
}
