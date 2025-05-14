using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

internal class TestSetConfiguration(ServiceConfiguration serviceConfiguration, string? path = null)
        : SetConfiguration(serviceConfiguration, path)
{
    internal Dictionary<Type, EndpointConfiguration> Endpoints => _endpoints;
}
