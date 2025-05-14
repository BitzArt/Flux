namespace BitzArt.Flux.REST.Endpoints;

internal sealed class PathEndpointConfiguration : EndpointConfiguration
{
    private readonly string? _path;
    private readonly List<Type> _operationTypes;

    public PathEndpointConfiguration(
        ServiceConfiguration serviceConfiguration,
        SetConfiguration setConfiguration,
        SetEndpointCollection endpointCollection,
        HttpMethods httpMethods,
        string? path)
        : base(serviceConfiguration, setConfiguration, endpointCollection, httpMethods)
    {
        _path = path;
        _operationTypes = [.. httpMethods.GetOperationTypes()];
    }

    public override IEnumerable<Type> OperationTypes => _operationTypes;

    public override HttpRequestMessage Resolve(OperationDescriptor descriptor)
    {
        var httpMethod = GetHttpMethod(descriptor);
        var path = GetPath(descriptor);
        var body = GetBody(descriptor);

        var requestMessage = new HttpRequestMessage(httpMethod, path);
        requestMessage.Headers.Accept.Add(new("application/json"));

        if (body is not null)
        {
            requestMessage.Content = body;
            requestMessage.Content.Headers.ContentType = new("application/json");
        }

        return requestMessage;
    }

    private HttpMethod GetHttpMethod(OperationDescriptor descriptor)
    {
        throw new NotImplementedException();
    }

    private string GetPath(OperationDescriptor descriptor)
    {
        throw new NotImplementedException();
    }

    private StringContent? GetBody(OperationDescriptor descriptor)
    {
        throw new NotImplementedException();
    }
}

internal static class PathEndpointConfigurationHttpMethodsExtensions
{
    public static List<Type> GetOperationTypes(this HttpMethods methods)
    {
        // Currently, there are 6 possible operation types,
        // hence the maximum capacity.
        var results = new List<Type>(6);

        if (methods.HasFlag(HttpMethods.Get))
        {
            results.Add(typeof(GetOperationDescriptor));
            results.Add(typeof(GetAllOperationDescriptor));
            results.Add(typeof(GetPageOperationDescriptor));
        }

        if (methods.HasFlag(HttpMethods.Post) || methods.HasFlag(HttpMethods.Put))
        {
            results.Add(typeof(AddOperationDescriptor));
        }

        if (methods.HasFlag(HttpMethods.Put) || methods.HasFlag(HttpMethods.Patch))
        {
            results.Add(typeof(UpdateOperationDescriptor));
        }

        return results;
    }
}
