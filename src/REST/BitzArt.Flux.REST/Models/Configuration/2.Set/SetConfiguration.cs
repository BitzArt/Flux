namespace BitzArt.Flux.REST;

internal class SetConfiguration
{
    public ServiceConfiguration ServiceConfiguration { get; private init; }
    public SetEndpointCollection Endpoints { get; private init; }

    public SetConfiguration(ServiceConfiguration serviceConfiguration, string? path = null)
    {
        ServiceConfiguration = serviceConfiguration;
        Endpoints = new(serviceConfiguration, this, path);
    }
}
