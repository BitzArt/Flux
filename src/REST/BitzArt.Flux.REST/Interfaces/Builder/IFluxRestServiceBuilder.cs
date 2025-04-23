using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

/// <summary>
/// Flux REST service builder.
/// </summary>
public interface IFluxRestServiceBuilder : IFluxServiceBuilder
{
    internal IFluxServiceBuilder SourceBuilder { get; }

    internal FluxRestServiceOptions ServiceOptions { get; }
    internal Action<IServiceProvider, HttpClient> HttpClientConfiguration { get; set; }

    IServiceCollection IFluxBuilder.ServiceCollection => SourceBuilder.ServiceCollection;
    IFluxBuilder IFluxServiceBuilder.FluxBuilder => SourceBuilder.FluxBuilder;
    string IFluxServiceBuilder.ServiceName => SourceBuilder.ServiceName;
}
