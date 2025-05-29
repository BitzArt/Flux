namespace BitzArt.Flux;

/// <summary>
/// Flux JSON service builder.
/// </summary>
public interface IFluxJsonServiceBuilder : IFluxServiceBuilder
{
    internal FluxJsonServiceOptions ServiceOptions { get; }
}