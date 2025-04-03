using BitzArt.Flux.REST;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxRestServiceBuilder : IFluxRestServiceBuilder
{
    public IServiceCollection ServiceCollection { get; private set; }
    public IFluxServiceRegistration ServiceFactory { get; set; }
    public IFluxFactory ServiceRegistration { get; init; }
    public FluxRestServiceOptions ServiceOptions { get; init; }
    public Action<IServiceProvider, HttpClient>? HttpClientConfiguration { get; set; }

    public FluxRestServiceBuilder(IFluxServicePreBuilder prebuilder, string? baseUrl)
    {
        ServiceCollection = prebuilder.ServiceCollection;
        ServiceRegistration = prebuilder.Factory;
        ServiceOptions = new(baseUrl);
        HttpClientConfiguration = null;

        if (prebuilder.Name is null) throw new Exception("Missing Name in Flux Service configuration. Please specify your external services' names when configuring Flux.");
        ServiceFactory = new FluxRestServiceFactory(ServiceOptions, prebuilder.Name);
    }
}
