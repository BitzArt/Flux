namespace BitzArt.Flux.REST;

internal class FluxRestSetConfiguration
{
    public FluxRestServiceConfiguration ServiceConfiguration { get; }

    /// <summary>
    /// Global path for the set (if any), relative to <see cref="FluxRestServiceConfiguration.BaseUrl"/>
    /// </summary>
    public string? Path { get; }

    public FluxRestSetEndpointCollection Endpoints { get; } = new();

    public FluxRestSetConfiguration(FluxRestServiceConfiguration serviceOptions, string? path = null)
    {
        ServiceConfiguration = serviceOptions;
        Path = path;
    }

    internal FluxRestSetEndpointInfo ResolveEndpoint(OperationDescriptor descriptor)
        => Endpoints.Resolve(descriptor, this);
}
