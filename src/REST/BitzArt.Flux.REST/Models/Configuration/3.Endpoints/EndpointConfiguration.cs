using System.Text.Json;

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
    /// HTTP methods provided with this configuration.
    /// </summary>
    internal readonly HttpMethods HttpMethods;

    public EndpointConfiguration(
        ServiceConfiguration serviceConfiguration,
        SetConfiguration setConfiguration,
        HttpMethods httpMethods)
    {
        ServiceConfiguration = serviceConfiguration;
        SetConfiguration = setConfiguration;

        HttpMethods = httpMethods;
    }

    /// <summary>
    /// Types of operation descriptors supported by this endpoint.
    /// </summary>
    public abstract IEnumerable<Type> OperationTypes { get; }

    public abstract HttpRequestMessage Resolve(OperationDescriptor descriptor);

    public virtual bool CanBeOverridden(EndpointConfiguration newConfiguration)
    {
        // Unless specified otherwise, an endpoint configuration
        // can be overridden by another endpoint configuration
        // during endpoint configuration phase
        // if the new configuration's HttpMethods are a subset
        // of the current configuration's HttpMethods
        if (newConfiguration.HttpMethods.IsSubsetOf(HttpMethods)) return true;

        return false;
    }

    protected StringContent? GetBody(OperationDescriptor descriptor)
    {
        if (descriptor is not ModelOperationDescriptor modelDescriptor) return null;

        if (modelDescriptor.Value is null) return null;

        var jsonSerializerOptions = ServiceConfiguration.SerializerOptions;

        var json = JsonSerializer.Serialize(modelDescriptor.Value, modelDescriptor.Value.GetType(), jsonSerializerOptions);

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        return content;
    }
}
