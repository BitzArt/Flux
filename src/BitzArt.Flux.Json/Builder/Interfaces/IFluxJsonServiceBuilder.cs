namespace BitzArt.Flux;

public interface IFluxJsonServiceBuilder : IFluxServiceBuilder
{
    public FluxJsonServiceOptions ServiceOptions { get; }
    internal string? BasePath { get; }
}