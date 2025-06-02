using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Linq;
using System.Text.Json;

namespace BitzArt.Flux.REST;

internal class HttpRequestMessageResolver(ILoggerFactory loggerFactory) : IHttpRequestMessageResolver
{
    private readonly ILogger _logger = loggerFactory.CreateLogger("Flux");

    public HttpRequestMessage Resolve(
        FluxRestSetConfiguration setConfiguration,
        string? endpointPath,
        OperationDescriptor descriptor,
        bool pathComplete = false,
        bool queryComplete = false)
    {
        var httpMethod = descriptor.GetExpectedHttpMethod();
        var uri = GetUri(setConfiguration, endpointPath, descriptor, pathComplete, queryComplete);
        
        var body = GetBody(descriptor, setConfiguration.ServiceConfiguration.JsonSerializerOptions);

        var requestMessage = new HttpRequestMessage(httpMethod, uri);
        requestMessage.Headers.Accept.Add(new("application/json"));
        requestMessage.Content = body;

        return requestMessage;
    }

    private Uri GetUri(
        FluxRestSetConfiguration setConfiguration,
        string? endpointPath,
        OperationDescriptor descriptor,
        bool pathComplete = false,
        bool queryComplete = false)
    {
        var path = pathComplete
            ? endpointPath
            : GetPath(setConfiguration.ServiceConfiguration.BasePath, setConfiguration.Path, endpointPath, descriptor);

        path = ApplyParameters(path, descriptor.Parameters, out var leftoverParameters, out var query);

        if (!queryComplete)
        {
            if (leftoverParameters is not null)
            {
                query = query + GetQueryString(leftoverParameters);
            }

            if (descriptor is GetPageOperationDescriptor pageDescriptor)
            {
                query = query + pageDescriptor.PageRequest.ToQueryString();
            }
        }

        var uri = new Uri($"{path}{query.Value}", UriKind.RelativeOrAbsolute);

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

    private string? ApplyParameters(
        string? path,
        IOperationParameterCollection? parameters,
        out List<KeyValuePair<string, object>>? leftoverParameters,
        out QueryString query)
    {
        if (parameters is null)
        {
            leftoverParameters = null;

            if (path is null)
            {
                query = QueryString.Empty;
                return null;
            }

            (path, query) = Split(path);
            return path;
        }

        return parameters switch
        {
            INamedOperationParameterCollection named => Replace(path, named, out leftoverParameters, out query),
            _ => Replace(path, parameters, out leftoverParameters, out query)
        };
    }

    private string? Replace(
        string? path,
        INamedOperationParameterCollection parameters,
        out List<KeyValuePair<string, object>> leftoverParameters,
        out QueryString query)
    {
        if (path is null)
        {
            query = QueryString.Empty;
            leftoverParameters = [.. parameters.Values];
            return null;
        }

        leftoverParameters = new(parameters.Values.Count());
        var appliedParameters = new List<KeyValuePair<string, string>>(parameters.Values.Count());

        foreach (var parameter in parameters.Values)
        {
            var value = parameter.Value.ToString() ?? string.Empty;
            var replaced = path.Replace($"{{{parameter.Key}}}", value);

            if (replaced == path)
            {
                leftoverParameters.Add(parameter);
            }
            else
            {
                path = replaced;
                appliedParameters.Add(new(parameter.Key, value));
            }
        }

        if (path.Contains('{'))
        {
            var startIndex = path.IndexOf('{');
            var endIndex = path.IndexOf('}', startIndex);
            var parameterName = (endIndex < 0
                ? null
                : path.Substring(startIndex + 1, endIndex - startIndex - 1))
                ?? throw new InvalidOperationException("Invalid endpoint path format");

            throw new InvalidOperationException($"Required parameter '{parameterName}' Is missing from the received parameter collection.");
        }

        if (appliedParameters.Count > 0 || leftoverParameters.Count > 0)
        {
            var replaced = appliedParameters.Count == 0
                    ? string.Empty
                    : "Replaced:\n" + string.Join('\n', appliedParameters.Select(kvp => $"[{kvp.Key}]: '{kvp.Value}';"));

            var queryString = leftoverParameters.Count == 0
                    ? string.Empty
                    : "QueryString:\n" + string.Join('\n', leftoverParameters.Select(kvp
                        => kvp.Value is IEnumerable enumerable
                        ? $"[{kvp.Key}]: {GetEnumerableParameterString(enumerable)}"
                        : $"[{kvp.Key}]: '{kvp.Value}';"));

            _logger.LogDebug("{replaced}\n{query}", replaced, queryString);
                
        }  

        (path, query) = Split(path);

        return path;
    }

    private static string GetEnumerableParameterString(IEnumerable enumerable)
    {
        var items = enumerable.Cast<object>().Select(item => item is null ? "'null'" : $"'{item}'");
        return $"[{string.Join(", ", items)}]";
    }

    private string? Replace(
        string? path,
        IOperationParameterCollection parameters,
        out List<KeyValuePair<string, object>>? leftoverParameters,
        out QueryString query)
    {
        leftoverParameters = null;

        if (path is null)
        {
            query = QueryString.Empty;
            return null;
        }

        var providedParameterCount = parameters.Values.Count();
        var lastIndex = providedParameterCount - 1;

        var appliedParameters = new List<KeyValuePair<string, string>>(providedParameterCount);

        var parameterCounter = 0;
        while(true)
        {
            var parameterStartIndex = path.IndexOf('{');
            if (parameterStartIndex < 0)
            {
                break; // No more parameters to replace
            }
            var parameterEndIndex = path.IndexOf('}', parameterStartIndex);
            if (parameterEndIndex < 0)
            {
                throw new InvalidOperationException("Unable to propagate parameters: Invalid path format.");
            }
            var parameterName = path.Substring(parameterStartIndex + 1, parameterEndIndex - parameterStartIndex - 1);

            if (parameterCounter > lastIndex)
            {
                throw new InvalidOperationException($"Unable to propagate parameter '{parameterName}'. No parameter values left.");
            }

            var parameterValue = parameters.Values.ElementAt(parameterCounter).ToString() ?? string.Empty;
            appliedParameters.Add(new KeyValuePair<string, string>(parameterName, parameterValue));

            path = path.Remove(parameterStartIndex, parameterEndIndex - parameterStartIndex + 1)
                       .Insert(parameterStartIndex, parameterValue);

            parameterCounter++;
        }

        if (appliedParameters.Count != providedParameterCount)
        {
            throw new InvalidOperationException($"Parameter count mismatch. Received {providedParameterCount} parameters, but only {appliedParameters.Count} are declared in endpoint path.");
        }

        if (appliedParameters.Count > 0)
        {
            _logger.LogDebug("{replaceLog}", "Replaced:\n" + string.Join('\n', appliedParameters.Select(kvp => $"[{kvp.Key}]: '{kvp.Value}';")));
        }

        (path, query) = Split(path);

        return path;
    }

    private static PathSplitResult Split(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return new(string.Empty, QueryString.Empty);
        }

        var queryIndex = path.IndexOf('?');

        if (queryIndex < 0)
        {
            return new(path, QueryString.Empty);
        }

        var queryString = new QueryString(path[queryIndex..]);
        var pathWithoutQuery = path[..queryIndex].TrimEnd('/');

        return new(pathWithoutQuery, queryString);
    }

    private record PathSplitResult(string Path, QueryString Query);

    private static QueryString GetQueryString(List<KeyValuePair<string, object>> leftoverParameters)
    {
        if (leftoverParameters.Count == 0)
        {
            return QueryString.Empty;
        }

        var query = new QueryString();
        foreach (var parameter in leftoverParameters)
        {
            query = parameter.Value switch
            {
                IEnumerable enumerable when enumerable is not string => query.Add(GetQueryString(enumerable)),
                _ => query.Add(parameter.Key, parameter.Value.ToString())
            };
        }

        return query;
    }

    private static QueryString GetQueryString(IEnumerable enumerableParameter)
    {
        var query = new QueryString();
        var index = 0;
        foreach (var item in enumerableParameter)
        {
            query = query.Add($"item[{index}]", item is not null ? item.ToString() : "null");
            index++;
        }
        return query;
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
