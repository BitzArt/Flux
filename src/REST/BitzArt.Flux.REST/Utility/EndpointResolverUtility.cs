using System.Diagnostics;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal static class EndpointResolverUtility
{
    public static HttpRequestMessage Resolve(
        SetConfiguration setConfiguration,
        string? endpointPath,
        OperationDescriptor descriptor)
    {
        var httpMethod = GetHttpMethod(descriptor);
        var path = GetPath(setConfiguration.ServiceConfiguration.BasePath, setConfiguration.Path, endpointPath, descriptor);
        var body = GetBody(descriptor, setConfiguration.ServiceConfiguration.JsonSerializerOptions);

        var requestMessage = new HttpRequestMessage(httpMethod, path);
        requestMessage.Headers.Accept.Add(new("application/json"));
        requestMessage.Content = body;

        return requestMessage;
    }

    private static HttpMethod GetHttpMethod(OperationDescriptor descriptor) => descriptor switch
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

    private static string GetPath(
        string? serviceBasePath,
        string? setPath,
        string? endpointPath,
        OperationDescriptor descriptor)
    {
        var parts = new List<string>(4);

        ConsiderPathPart(parts, serviceBasePath);
        ConsiderPathPart(parts, setPath);
        ConsiderPathPart(parts, endpointPath);
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

        return keyedDescriptor.Id?.ToString();
    }

    private static StringContent? GetBody(OperationDescriptor descriptor, JsonSerializerOptions jsonSerializerOptions)
    {
        if (descriptor is not ModelOperationDescriptor modelDescriptor) return null;

        if (modelDescriptor.Value is null) return null;

        var json = JsonSerializer.Serialize(modelDescriptor.Value, modelDescriptor.Value.GetType(), jsonSerializerOptions);

        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        return content;
    }
}
