using System.Diagnostics;

namespace BitzArt.Flux.REST.Endpoints;

internal sealed class PathEndpointConfiguration : EndpointConfiguration
{
    private readonly string? _path;
    private readonly List<Type> _operationTypes;

    public PathEndpointConfiguration(
        ServiceConfiguration serviceConfiguration,
        SetConfiguration setConfiguration,
        HttpMethods httpMethods,
        string? path)
        : base(serviceConfiguration, setConfiguration, httpMethods)
    {
        _path = path?.TrimEnd('/');

        _operationTypes = GetOperationTypes(httpMethods);
    }

    public override IEnumerable<Type> OperationTypes => _operationTypes.AsReadOnly();

    public override HttpRequestMessage Resolve(OperationDescriptor descriptor)
    {
        var httpMethod = GetHttpMethod(descriptor);
        var path = GetPath(descriptor);
        var body = GetBody(descriptor);

        var requestMessage = new HttpRequestMessage(httpMethod, path);
        requestMessage.Headers.Accept.Add(new("application/json"));
        requestMessage.Content = body;

        return requestMessage;
    }

    private static HttpMethod GetHttpMethod(OperationDescriptor descriptor)
        => descriptor switch
        {
            GetOperationDescriptor => HttpMethod.Get,

            GetAllOperationDescriptor => HttpMethod.Get,

            GetPageOperationDescriptor => HttpMethod.Get,

            AddOperationDescriptor addOperationDescriptor
                => addOperationDescriptor.Id is null ? HttpMethod.Post : HttpMethod.Put,

            UpdateOperationDescriptor updateOperationDescriptor
                => updateOperationDescriptor.Partial ? HttpMethod.Patch : HttpMethod.Put,

            RemoveOperationDescriptor => HttpMethod.Delete,

            _ => throw new UnreachableException($"Unsupported operation type: {descriptor.GetType().Name}.")
        };

    private string GetPath(OperationDescriptor descriptor)
    {
        var parts = new List<string>(4);

        ConsiderPathPart(parts, ServiceConfiguration.BasePath);
        ConsiderPathPart(parts, SetConfiguration.Path);
        ConsiderPathPart(parts, _path);
        ConsiderPathPart(parts, GetIdPart(descriptor));

        var path = string.Join('/', parts);

        if (string.IsNullOrWhiteSpace(path))
        {
            throw new InvalidOperationException("Path cannot be empty.");
        }

        return path;
    }

    private static void ConsiderPathPart(List<string> parts, string? part)
    {
        if (string.IsNullOrEmpty(part)) return;

        parts.Add(part);
    }

    private static string? GetIdPart(OperationDescriptor descriptor)
    {
        if (descriptor is not KeyedOperationDescriptor keyedDescriptor) return null;

        if (keyedDescriptor.Id is null) return null;

        return keyedDescriptor.Id.ToString();
    }

    private static List<Type> GetOperationTypes(HttpMethods methods)
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

        if (methods.HasFlag(HttpMethods.Delete))
        {
            results.Add(typeof(RemoveOperationDescriptor));
        }

        return results;
    }
}
