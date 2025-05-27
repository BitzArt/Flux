using BitzArt.Flux.REST;
using Microsoft.AspNetCore.Http;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for converting operation descriptors to query strings.
/// </summary>
public static class ToQueryStringExtension
{
    /// <summary>
    /// Converts the <see cref="OperationDescriptor"/> to a query string representation.
    /// </summary>
    /// <param name="descriptor">Operation descriptor to convert.</param>
    /// <returns>A query string with the leading '?' character, or an empty string if no parameters are present.</returns>
    public static QueryString GetQueryString(this OperationDescriptor descriptor)
    {
        var query = new QueryString();

        var parameters = GetParameters(descriptor.Parameters);
        if (parameters is not null)
        {
            query = query.Add(parameters!.Value);
        }

        var operationSpecific = GetOperationSpecific(descriptor);
        if (operationSpecific is not null)
        {
            query = query.Add(operationSpecific!.Value);
        }

        return query;
    }

    private static QueryString? GetParameters(IOperationParameterCollection? parameters)
    {
        QueryString? query = null;

        if (parameters is null)
        {
            return query;
        }

        if (parameters is INamedOperationParameterCollection named)
        {
            if (named.Values.Any())
            {
                query ??= new();
            }

            foreach (var kvp in named.Values)
            {
                query = query!.Value.Add(kvp.Key, kvp.Value?.ToString() ?? "null");
            }

            return query;
        }

        throw new NotSupportedException("Implicit query string conversion is only supported for named parameter collections.");
    }

    private static QueryString? GetOperationSpecific(OperationDescriptor descriptor)
    {
        QueryString? query = null;

        if (descriptor is GetPageOperationDescriptor getPage)
        {
            var pageRequestPart = getPage.PageRequest.ToQueryString();

            if (pageRequestPart.HasValue)
            {
                query ??= new();
                query = query!.Value.Add(pageRequestPart);
            }
        }

        return query;
    }
}
