using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

internal class CommunicatorRestServiceBuilder : ICommunicatorRestServiceBuilder
{
    public IServiceCollection Services { get; private set; }
    public ICommunicatorServiceProvider Provider { get; set; }
    public ICommunicatorServiceFactory Factory { get; init; }
    public CommunicatorRestServiceOptions ServiceOptions { get; init; }
    public Action<HttpClient>? HttpClientConfiguration { get; set; }

    public CommunicatorRestServiceBuilder(ICommunicatorServicePreBuilder prebuilder, string baseUrl)
    {
        Services = prebuilder.Services;
        Factory = prebuilder.Factory;
        ServiceOptions = new();
        HttpClientConfiguration = null;

        if (prebuilder.Name is null) throw new Exception("Missing Name in Communication Service configuration. Consider using .WithName() when configuring external services.");
        if (string.IsNullOrWhiteSpace(baseUrl)) throw new Exception($"Missing BaseUrl for service '{prebuilder.Name}'");
        Provider = new CommunicatorRestServiceProvider(ServiceOptions, prebuilder.Name, baseUrl);
    }
}
