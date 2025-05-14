namespace BitzArt.Flux.REST.Endpoints;

internal sealed class ResolverEndpointConfiguration<TOperation> : EndpointConfiguration
    where TOperation : OperationDescriptor
{
    public override IEnumerable<Type> OperationTypes => [typeof(TOperation)];

    private readonly Func<TOperation, HttpRequestMessage> _resolver;

    public ResolverEndpointConfiguration(
        ServiceConfiguration serviceConfiguration,
        SetConfiguration setConfiguration,
        SetEndpointCollection endpointCollection,
        HttpMethods httpMethods,
        Func<TOperation, HttpRequestMessage> resolver)
        : base(serviceConfiguration, setConfiguration, endpointCollection, httpMethods)
    {
        _resolver = resolver;
    }

    public override HttpRequestMessage Resolve(OperationDescriptor descriptor)
    {
        if (descriptor is not TOperation operation)
        {
            throw new InvalidOperationException($"Invalid operation type: {descriptor.GetType().Name}. Expected: {typeof(TOperation).Name}.");
        }

        return _resolver.Invoke(operation);
    }

    public override bool CanBeOverridden(EndpointConfiguration newConfiguration) => false;
}
