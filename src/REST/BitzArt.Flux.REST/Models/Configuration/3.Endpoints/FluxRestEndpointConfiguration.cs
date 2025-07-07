namespace BitzArt.Flux.Rest.Endpoints;

internal abstract class FluxRestEndpointConfiguration
{
    /// <summary>
    /// Owner service's configuration.
    /// </summary>
    protected readonly FluxRestServiceConfiguration ServiceConfiguration;

    /// <summary>
    /// Owner set's configuration.
    /// </summary>
    protected readonly FluxRestSetConfiguration SetConfiguration;

    /// <summary>
    /// HTTP methods provided with this configuration.
    /// </summary>
    internal readonly HttpMethods HttpMethods;

    public FluxRestEndpointConfiguration(
        FluxRestSetConfiguration setConfiguration,
        HttpMethods httpMethods)
    {
        SetConfiguration = setConfiguration;
        ServiceConfiguration = setConfiguration.ServiceConfiguration;

        HttpMethods = httpMethods;
    }

    /// <summary>
    /// Types of operation descriptors supported by this endpoint.
    /// </summary>
    public abstract IEnumerable<Type> OperationTypes { get; }

    public virtual IEnumerable<HttpMethod> SupportedHttpMethods => HttpMethods.ToHttpMethods();

    public abstract HttpRequestMessage Resolve(OperationDescriptor descriptor, IServiceProvider serviceProvider);

    public virtual bool CanBeOverridden(FluxRestEndpointConfiguration newConfiguration)
    {
        // Unless specified otherwise, an endpoint configuration
        // can be overridden by another endpoint configuration
        // during endpoint configuration phase
        // if the new configuration's HttpMethods are a subset
        // of the current configuration's HttpMethods
        if (newConfiguration.SupportedHttpMethods.IsSubsetOf(SupportedHttpMethods)) return true;

        return false;
    }
}
