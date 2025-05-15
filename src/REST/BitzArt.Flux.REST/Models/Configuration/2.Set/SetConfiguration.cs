using BitzArt.Flux.REST.Endpoints;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

internal class SetConfiguration
{
    private record EndpointResolverSignature(Type OperationType, HttpMethod HttpMethod);

    private readonly Dictionary<EndpointResolverSignature, EndpointConfiguration> _endpoints;

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
        var supportedHttpMethods = configuration.SupportedHttpMethods;

        foreach (var operationType in supportedOperationTypes)
        {
            foreach (var httpMethod in supportedHttpMethods)
            {
                Add(configuration, operationType, httpMethod);
            }
        }
    }

    private void Add(EndpointConfiguration configuration, Type operationType, HttpMethod httpMethod)
    {
        var signature = new EndpointResolverSignature(operationType, httpMethod);

        if (_endpoints.TryGetValue(signature, out var existingConfig))
        {
            // Existing configuration found for the operation type,
            // considering replacing it with a new one.
            ConsiderReplace(existingConfig, configuration, signature);
            return;
        }

        // No existing configuration found for the operation type,
        // adding the new one.
        _endpoints[signature] = configuration;
    }

    private void ConsiderReplace(EndpointConfiguration existingConfiguration, EndpointConfiguration newConfiguration, EndpointResolverSignature signature)
    {
        if (!existingConfiguration.CanBeOverridden(newConfiguration)) return;

        if (existingConfiguration.HttpMethods == newConfiguration.HttpMethods
            && existingConfiguration.GetType() == newConfiguration.GetType())
        {
            throw new InvalidOperationException($"Cannot replace existing endpoint configuration. " +
                $"The new configuration is identical to the existing one in terms of endpoint resolver hierarchy.");
        }

        _endpoints[signature] = newConfiguration;
    }

    public HttpRequestMessage Resolve(OperationDescriptor descriptor, IServiceProvider serviceProvider)
    {
        var operationType = descriptor.GetType();
        var expectedHttpMethod = descriptor.GetExpectedHttpMethod();

        var signature = new EndpointResolverSignature(operationType, expectedHttpMethod);

        if (_endpoints.TryGetValue(signature, out var endpointConfiguration))
        {
            return endpointConfiguration.Resolve(descriptor, serviceProvider);
        }

        var resolver = serviceProvider.GetRequiredService<IHttpRequestMessageResolver>();
        return resolver.Resolve(this, null, descriptor);
    }
}
