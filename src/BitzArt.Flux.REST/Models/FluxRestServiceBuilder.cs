using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxRestServiceBuilder : IFluxRestServiceBuilder
{
    public IServiceCollection Services { get; private set; }
    public IFluxServiceProvider ServiceProvider { get; set; }
    public IFluxProvider Provider { get; init; }
    public FluxRestServiceOptions ServiceOptions { get; init; }
    public Action<HttpClient>? HttpClientConfiguration { get; set; }

    public FluxRestServiceBuilder(IFluxServicePreBuilder prebuilder, string? baseUrl)
    {
        Services = prebuilder.Services;
        Provider = prebuilder.Factory;
        ServiceOptions = new(baseUrl);
        HttpClientConfiguration = null;

        if (prebuilder.Name is null) throw new Exception("Missing Name in Flux Service configuration. Consider using .WithName() when configuring external services.");
        ServiceProvider = new FluxRestServiceProvider(ServiceOptions, prebuilder.Name);
    }
}
