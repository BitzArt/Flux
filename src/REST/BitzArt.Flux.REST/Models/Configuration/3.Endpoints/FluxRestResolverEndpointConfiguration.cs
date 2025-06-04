namespace BitzArt.Flux.REST.Endpoints;

internal sealed class FluxRestResolverEndpointConfiguration<TOperation> : FluxRestEndpointConfiguration
    where TOperation : OperationDescriptor
{
    private readonly IEnumerable<Type> _supportedOperationTypes;
    public override IEnumerable<Type> OperationTypes => _supportedOperationTypes;

    private readonly IEnumerable<HttpMethod> _supportedHttpMethods;
    public override IEnumerable<HttpMethod> SupportedHttpMethods => _supportedHttpMethods;

    private readonly Func<TOperation, IServiceProvider, HttpRequestMessage> _resolver;

    public FluxRestResolverEndpointConfiguration(
        FluxRestSetConfiguration setConfiguration,
        HttpMethods httpMethods,
        Func<TOperation, IServiceProvider, HttpRequestMessage> resolver)
        : base(setConfiguration, httpMethods)
    {
        _resolver = resolver;

        _supportedOperationTypes = [.. OperationDescriptor.Types.Concrete
            .Where(type => type
                .IsAssignableTo(typeof(TOperation)))];

        _supportedHttpMethods = typeof(TOperation)
            .GetSupportedHttpMethods()
            .FilterBy(HttpMethods);
    }

    public override HttpRequestMessage Resolve(OperationDescriptor descriptor, IServiceProvider serviceProvider)
    {
        if (descriptor is not TOperation operation)
        {
            throw new InvalidOperationException($"Invalid operation type: {descriptor.GetType().Name}. Expected: {typeof(TOperation).Name}.");
        }

        return _resolver.Invoke(operation, serviceProvider);
    }

    public override bool CanBeOverridden(FluxRestEndpointConfiguration newConfiguration)
    {
        var newConfigurationType = newConfiguration.GetType();

        // only allow replacing by another ResolverEndpointConfiguration
        if (!newConfigurationType.IsGenericType) return false;
        if (newConfigurationType.GetGenericTypeDefinition() != typeof(FluxRestResolverEndpointConfiguration<>)) return false;

        if (!newConfiguration.SupportedHttpMethods.IsSubsetOf(SupportedHttpMethods)) return false;

        return true;
    }
}
