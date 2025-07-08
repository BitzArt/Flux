using BitzArt.Pagination;
using Microsoft.AspNetCore.Http;

namespace BitzArt.Flux.Rest;

/// <summary>
/// Extension methods for <see cref="PageRequest"/>.
/// </summary>
public static class PageRequestExtensions
{
    /// <summary>
    /// Converts a <see cref="PageRequest"/> to a <see cref="QueryString"/>.
    /// </summary>
    /// <param name="pageRequest">A <see cref="PageRequest"/> instance.</param>
    /// <returns>A <see cref="QueryString"/> representing the <see cref="PageRequest"/>.</returns>
    public static QueryString ToQueryString(this PageRequest pageRequest)
    {
        var queryString = QueryString.Empty;

        if (pageRequest.Offset is not null)
        {
            queryString = queryString.Add("offset", pageRequest.Offset.ToString());
        }

        if (pageRequest.Limit is not null)
        {
            queryString = queryString.Add("limit", pageRequest.Limit.ToString());
        }

        return queryString;
    }
}
