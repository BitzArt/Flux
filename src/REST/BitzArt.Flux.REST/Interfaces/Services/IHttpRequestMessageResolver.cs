namespace BitzArt.Flux.REST;

internal interface IHttpRequestMessageResolver
{
    public HttpRequestMessage Resolve(FluxRestSetConfiguration setConfiguration, string? endpointPath, OperationDescriptor descriptor, bool allIncluded = false);
}