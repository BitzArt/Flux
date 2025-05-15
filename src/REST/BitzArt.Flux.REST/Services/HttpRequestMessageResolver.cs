using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class HttpRequestMessageResolver : IHttpRequestMessageResolver
{
    public HttpRequestMessage Resolve(
        SetConfiguration setConfiguration,
        string? endpointPath,
        OperationDescriptor descriptor)
    {
        var httpMethod = descriptor.GetExpectedHttpMethod();
        var path = GetPath(setConfiguration.ServiceConfiguration.BasePath, setConfiguration.Path, endpointPath, descriptor);
        var queryString = GetQueryString(descriptor);
        var uri = new Uri($"{path}{queryString}", UriKind.RelativeOrAbsolute);
        var body = GetBody(descriptor, setConfiguration.ServiceConfiguration.JsonSerializerOptions);

        var requestMessage = new HttpRequestMessage(httpMethod, uri);
        requestMessage.Headers.Accept.Add(new("application/json"));
        requestMessage.Content = body;

        return requestMessage;
    }

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

    private static QueryString GetQueryString(OperationDescriptor descriptor)
        => descriptor switch
        {
            GetPageOperationDescriptor getPageOperationDescriptor => getPageOperationDescriptor.PageRequest.ToQueryString(),

            OperationDescriptor => QueryString.Empty,

            _ => throw new UnreachableException($"Unsupported operation type: {descriptor.GetType().Name}.")
        };

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
