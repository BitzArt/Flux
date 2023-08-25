using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

internal class RestCommunicatorServiceBuilder : IRestCommunicatorServiceBuilder
{
    public IServiceCollection Services { get; private set; }
    public ICommunicatorServiceProvider Provider { get; set; }
    public ICommunicatorServiceFactory Factory { get; init; }

    public RestCommunicatorServiceBuilder(ICommunicatorServicePreBuilder prebuilder, string baseUrl)
    {
        Services = prebuilder.Services;
        Factory = prebuilder.Factory;

        if (prebuilder.Name is null) throw new Exception("Missing Name in Communication Service configuration. Consider using .WithName() when configuring external services.");
        if (string.IsNullOrWhiteSpace(baseUrl)) throw new Exception($"Missing BaseUrl for service '{prebuilder.Name}'");
        Provider = new RestCommunicatorServiceProvider(prebuilder.Name, baseUrl);
    }
}
