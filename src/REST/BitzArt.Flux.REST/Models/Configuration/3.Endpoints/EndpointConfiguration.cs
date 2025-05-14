namespace BitzArt.Flux.REST.Endpoints;

internal abstract class EndpointConfiguration
{
    /// <summary>
    /// Owner service's configuration.
    /// </summary>
    protected readonly ServiceConfiguration ServiceConfiguration;

    /// <summary>
    /// Owner set's configuration.
    /// </summary>
    protected readonly SetConfiguration SetConfiguration;

    /// <summary>
    /// Collection of endpoints for this set.
    /// </summary>
    protected readonly SetEndpointCollection EndpointCollection;

    /// <summary>
    /// HTTP methods provided with this configuration.
    /// </summary>
    internal readonly HttpMethods HttpMethods;

    public EndpointConfiguration(
        ServiceConfiguration serviceConfiguration,
        SetConfiguration setConfiguration,
        SetEndpointCollection endpointCollection,
        HttpMethods httpMethods)
    {
        ServiceConfiguration = serviceConfiguration;
        SetConfiguration = setConfiguration;
        EndpointCollection = endpointCollection;

        HttpMethods = httpMethods;
    }

    /// <summary>
    /// Types of operation descriptors supported by this endpoint.
    /// </summary>
    public abstract IEnumerable<Type> OperationTypes { get; }

    public abstract HttpRequestMessage Resolve(OperationDescriptor descriptor);

    public virtual bool CanBeOverridden(EndpointConfiguration newConfiguration)
    {
        // Can be overridden by another configuration if the new configuration's
        // HttpMethods are a subset of the current configuration's HttpMethods
        if (newConfiguration.HttpMethods.IsSubsetOf(HttpMethods)) return true;

        return false;
    }
}
