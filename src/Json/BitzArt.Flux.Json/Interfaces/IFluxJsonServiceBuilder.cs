namespace BitzArt.Flux;

public interface IFluxJsonServiceBuilder : IFluxServiceBuilder
{
    internal FluxJsonServiceOptions ServiceOptions { get; }
}