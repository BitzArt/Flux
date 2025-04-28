namespace BitzArt.Flux.REST;

internal class SetOptions
{
    public ServiceOptions ServiceOptions { get; }

    /// <summary>
    /// Global path for the set (if any), relative to <see cref="ServiceOptions.BaseUrl"/>
    /// </summary>
    public string? Path { get; }

    public SetEndpointCollection EndpointCollection { get; } = new();

    public SetOptions(ServiceOptions serviceOptions, string? path = null)
    {
        ServiceOptions = serviceOptions;
        Path = path;
    }
}
