namespace BitzArt.Flux;

public interface IFluxRestServiceBuilder : IFluxServiceBuilder
{
    public FluxRestServiceOptions ServiceOptions { get; }
    internal Action<HttpClient>? HttpClientConfiguration { get; set; }
}
