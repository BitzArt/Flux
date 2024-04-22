namespace BitzArt.Flux;

public interface IFluxRestServiceBuilder : IFluxServiceBuilder
{
    public FluxRestServiceOptions ServiceOptions { get; }
    internal Action<IServiceProvider, HttpClient>? HttpClientConfiguration { get; set; }
}
