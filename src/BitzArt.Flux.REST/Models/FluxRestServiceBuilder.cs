using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxRestServiceBuilder : IFluxRestServiceBuilder
{
    public IServiceCollection Services { get; private set; }
    public IFluxServiceFactory ServiceFactory { get; set; }
    public IFluxFactory Factory { get; init; }
    public FluxRestServiceOptions ServiceOptions { get; init; }
    public Action<IServiceProvider, HttpClient>? HttpClientConfiguration { get; set; }

    public FluxRestServiceBuilder(IFluxServicePreBuilder prebuilder, string? baseUrl)
    {
        Services = prebuilder.Services;
        Factory = prebuilder.Factory;
        ServiceOptions = new(baseUrl);
        HttpClientConfiguration = null;

        if (prebuilder.Name is null) throw new Exception("Missing Name in Flux Service configuration. Please specify your external services' names when configuring Flux.");
        ServiceFactory = new FluxRestServiceFactory(ServiceOptions, prebuilder.Name);
    }
}
