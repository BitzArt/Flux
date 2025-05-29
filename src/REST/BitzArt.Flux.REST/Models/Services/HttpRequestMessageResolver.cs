using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class HttpRequestMessageResolver : IHttpRequestMessageResolver
{
    public HttpRequestMessage Resolve(
        FluxRestSetConfiguration setConfiguration,
        string? endpointPath,
        OperationDescriptor descriptor,
        bool allIncluded = false)
    {
        var httpMethod = descriptor.GetExpectedHttpMethod();
        var uri = GetUri(setConfiguration, endpointPath, descriptor, allIncluded);
        
        var body = GetBody(descriptor, setConfiguration.ServiceConfiguration.JsonSerializerOptions);

        var requestMessage = new HttpRequestMessage(httpMethod, uri);
        requestMessage.Headers.Accept.Add(new("application/json"));
        requestMessage.Content = body;

        return requestMessage;
    }

    private static Uri GetUri(
        FluxRestSetConfiguration setConfiguration,
        string? endpointPath,
        OperationDescriptor descriptor,
        bool allIncluded)
    {
        if (allIncluded)
        {
            return new Uri(endpointPath!, UriKind.RelativeOrAbsolute);
        }

        var path = GetPath(setConfiguration.ServiceConfiguration.BasePath, setConfiguration.Path, endpointPath, descriptor);
        var queryString = descriptor.GetQueryString();
        var uri = new Uri($"{path}{queryString}", UriKind.RelativeOrAbsolute);

        return uri;
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
