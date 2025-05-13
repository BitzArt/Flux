using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

internal class SetConfiguration<TModel, TKey>
    where TModel : class
{
    public ServiceConfiguration ServiceConfiguration { get; }

    /// <summary>
    /// Global path for the set (if any), relative to <see cref="ServiceConfiguration.BaseUrl"/>
    /// </summary>
    public string? Path { get; }

    public SetEndpointCollection<TModel, TKey> Endpoints { get; }

    public SetConfiguration(ServiceConfiguration serviceOptions, string? path = null)
    {
        ServiceConfiguration = serviceOptions;
        Path = path;

        Endpoints = new(this);
    }

    internal HttpRequestMessage ResolveHttpRequest(OperationDescriptor descriptor)
        => Endpoints.Resolve(descriptor);
}
