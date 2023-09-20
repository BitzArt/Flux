using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxRestServiceBuilder : IFluxRestServiceBuilder
{
    public IServiceCollection Services { get; private set; }
    public IFluxServiceContext ServiceContext { get; set; }
    public IFluxServiceFactory Factory { get; init; }
    public FluxRestServiceOptions ServiceOptions { get; init; }
    public Action<HttpClient>? HttpClientConfiguration { get; set; }

    public FluxRestServiceBuilder(IFluxServicePreBuilder prebuilder, string? baseUrl)
    {
        Services = prebuilder.Services;
        Factory = prebuilder.Factory;
        ServiceOptions = new(baseUrl);
        HttpClientConfiguration = null;

        if (prebuilder.Name is null) throw new Exception("Missing Name in Flux Service configuration. Consider using .WithName() when configuring external services.");
        ServiceContext = new FluxRestServiceContext(ServiceOptions, prebuilder.Name);
    }
}
