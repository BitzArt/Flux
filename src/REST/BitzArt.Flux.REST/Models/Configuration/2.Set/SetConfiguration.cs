using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

internal class SetConfiguration
{
    private protected readonly Dictionary<Type, EndpointConfiguration> _endpoints;

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
        var supportedOperationTypes = configuration.OperationTypes;

        foreach (var operationType in supportedOperationTypes)
        {
            Add(configuration, operationType);
        }
    }

    private void Add(EndpointConfiguration configuration, Type operationType)
    {
        if (_endpoints.TryGetValue(operationType, out var existingConfig))
        {
            // Existing configuration found for the operation type,
            // considering replacing it with the new one.
            ConsiderReplace(existingConfig, configuration, operationType);
            return;
        }

        // No existing configuration found for the operation type,
        // adding the new one.
        _endpoints[operationType] = configuration;
    }

    private void ConsiderReplace(EndpointConfiguration existingConfiguration, EndpointConfiguration newConfiguration, Type operationType)
    {
        if (!existingConfiguration.CanBeOverridden(newConfiguration)) return;

        if (existingConfiguration.HttpMethods == newConfiguration.HttpMethods
            && existingConfiguration.GetType() == newConfiguration.GetType())
        {
            throw new InvalidOperationException($"Cannot replace existing endpoint configuration. " +
                $"The new configuration is identical to the existing one in terms of endpoint resolver hierarchy.");
        }

        _endpoints[operationType] = newConfiguration;
    }

    public HttpRequestMessage Resolve(OperationDescriptor descriptor)
    {
        var operationType = descriptor.GetType();

        if (_endpoints.TryGetValue(operationType, out var endpointConfiguration))
        {
            return endpointConfiguration.Resolve(descriptor);
        }

        return EndpointResolverUtility.Resolve(this, null, descriptor);
    }
}
