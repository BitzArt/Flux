using BitzArt.Flux.REST;
using Microsoft.AspNetCore.Http;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for converting operation descriptors to query strings.
/// </summary>
public static class ToQueryStringExtension
{
    /// <summary>
    /// Builds a <see cref="QueryString"/> for the given <see cref="OperationDescriptor"/>.
    /// The resulting query string will include named parameters
    /// if provided parameter collection implements <see cref="INamedOperationParameterCollection"/>,
    /// as well as operation-specific parameters such as pagination parameters for <see cref="GetPageOperationDescriptor"/>.
    /// </summary>
    /// <param name="descriptor">Operation descriptor to convert.</param>
    /// <returns>A <see cref="QueryString"/> representing HTTP query parameters in this operation.</returns>
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
