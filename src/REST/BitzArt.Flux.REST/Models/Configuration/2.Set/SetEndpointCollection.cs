using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

internal class SetEndpointCollection
{
    private readonly ServiceConfiguration _serviceConfiguration;
    private readonly SetConfiguration _setConfiguration;

    private readonly string? _path;
    private readonly Dictionary<Type, EndpointConfiguration> _endpoints;

    public SetEndpointCollection(ServiceConfiguration serviceConfiguration, SetConfiguration setConfiguration, string? path)
    {
        _serviceConfiguration = serviceConfiguration;
        _setConfiguration = setConfiguration;

        _path = path;

        _endpoints = [];
    }

    public void ConfigureEndpoint(EndpointConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    public HttpRequestMessage Resolve(OperationDescriptor descriptor)
    {
        throw new NotImplementedException();
    }
}
