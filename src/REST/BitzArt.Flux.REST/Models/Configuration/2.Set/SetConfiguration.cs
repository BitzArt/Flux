using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

internal class SetConfiguration
{
    private readonly Dictionary<Type, EndpointConfiguration> _endpoints;

    public ServiceConfiguration ServiceConfiguration { get; private init; }

    internal string? Path { get; private init; }

    public SetConfiguration(ServiceConfiguration serviceConfiguration, string? path = null)
    {
        ServiceConfiguration = serviceConfiguration;

        Path = path?.TrimEnd('/');

        _endpoints = [];
    }

    public void Add(EndpointConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    public HttpRequestMessage Resolve(OperationDescriptor descriptor)
    {
        throw new NotImplementedException();
    }
}
