namespace BitzArt.Flux;

/// <summary>
/// Flux REST service builder.
/// </summary>
public interface IFluxRestServiceBuilder : IFluxServiceBuilder
{
    internal FluxRestServiceOptions ServiceOptions { get; }
    internal Action<IServiceProvider, HttpClient>? HttpClientConfiguration { get; set; }
}
