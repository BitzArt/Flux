namespace BitzArt.Flux.REST;

internal interface IHttpRequestMessageResolver
{
    public HttpRequestMessage Resolve(SetConfiguration setConfiguration, string? endpointPath, OperationDescriptor descriptor, bool allIncluded = false);
}