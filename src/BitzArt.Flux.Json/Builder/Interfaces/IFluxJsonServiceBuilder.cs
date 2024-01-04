namespace BitzArt.Flux;

public interface IFluxJsonServiceBuilder : IFluxServiceBuilder
{
    public FluxJsonServiceOptions ServiceOptions { get; }
    public string? BasePath { get; }
}