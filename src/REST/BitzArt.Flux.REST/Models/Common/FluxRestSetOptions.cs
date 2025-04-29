namespace BitzArt.Flux.REST;

internal class FluxRestSetOptions
{
    public FluxRestServiceOptions ServiceOptions { get; }

    /// <summary>
    /// Global path for the set (if any), relative to <see cref="FluxRestServiceOptions.BaseUrl"/>
    /// </summary>
    public string? Path { get; }

    public FluxRestSetEndpointCollection EndpointCollection { get; } = new();

    public FluxRestSetOptions(FluxRestServiceOptions serviceOptions, string? path = null)
    {
        ServiceOptions = serviceOptions;
        Path = path;
    }
}
