using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

internal class SetConfiguration
{
    public ServiceConfiguration ServiceConfiguration { get; }

    /// <summary>
    /// Global path for the set (if any), relative to <see cref="ServiceConfiguration.BaseUrl"/>
    /// </summary>
    public string? Path { get; }

    public SetEndpointCollection Endpoints { get; }

    public SetConfiguration(ServiceConfiguration serviceOptions, string? path = null)
    {
        ServiceConfiguration = serviceOptions;
        Path = path;

        Endpoints = new(this);
    }
}
