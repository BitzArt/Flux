namespace BitzArt.Flux.REST;

internal interface IHttpRequestMessageResolver
{
    public HttpRequestMessage Resolve(
        FluxRestSetConfiguration setConfiguration,
        string? endpointPath,
        OperationDescriptor descriptor)
        => Resolve(setConfiguration, endpointPath, descriptor, false, false);

    public HttpRequestMessage Resolve(
        FluxRestSetConfiguration setConfiguration,
        string? endpointPath,
        OperationDescriptor descriptor,
        bool pathComplete,
        bool queryComplete);
}