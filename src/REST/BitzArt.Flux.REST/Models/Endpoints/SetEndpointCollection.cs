namespace BitzArt.Flux.REST.Endpoints;

internal class SetEndpointCollection
{
    private readonly SetConfiguration _setConfiguration;

    public SetEndpointCollection(SetConfiguration setConfiguration)
    {
        _setConfiguration = setConfiguration;
    }

    public HttpRequestMessage Resolve(OperationDescriptor descriptor)
    {
        throw new NotImplementedException();
    }
}
