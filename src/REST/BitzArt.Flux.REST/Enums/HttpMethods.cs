namespace BitzArt.Flux.REST;

/// <summary>
/// Represents the HTTP methods that can be used in a RESTful API.
/// </summary>
[Flags]
public enum HttpMethods
{
    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/GET">GET</see> http method.
    /// </summary>
    Get = 1 << 0,

    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/HEAD">HEAD</see> http method.
    /// </summary>
    Head = 1 << 1,

    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/POST">POST</see> http method.
    /// </summary>
    Post = 1 << 2,

    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/PUT">PUT</see> http method.
    /// </summary>
    Put = 1 << 3,

    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/DELETE">DELETE</see> http method.
    /// </summary>
    Delete = 1 << 4,

    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/CONNECT">CONNECT</see> http method.
    /// </summary>
    Connect = 1 << 5,

    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/OPTIONS">OPTIONS</see> http method.
    /// </summary>
    Options = 1 << 6,

    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/TRACE">TRACE</see> http method.
    /// </summary>
    Trace = 1 << 7,

    /// <summary>
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Methods/PATCH">PATCH</see> http method.
    /// </summary>
    Patch = 1 << 8,

    /// <summary>
    /// Combines all HTTP methods.
    /// </summary>
    All = Get | Head | Post | Put | Delete | Connect | Options | Trace | Patch
}

/// <summary>
/// Extension methods for <see cref="HttpMethods"/> enum.
/// </summary>
public static class HttpMethodsExtensions
{
    /// <summary>
    /// Determines whether the source <see cref="HttpMethods"/> is a subset of the target <see cref="HttpMethods"/>.
    /// </summary>
    /// <param name="source">Source <see cref="HttpMethods"/>.</param>
    /// <param name="target">Target <see cref="HttpMethods"/>.</param>
    /// <returns><see langword="true"/> if the source is a subset of the target or equal to it; otherwise, <see langword="false"/>.</returns>
    public static bool IsSubsetOf(this HttpMethods source, HttpMethods target)
    {
        return (source & target) == source;
    }

    private static readonly IReadOnlyDictionary<HttpMethods, HttpMethod> _map =
        new Dictionary<HttpMethods, HttpMethod>
        {
            [HttpMethods.Get] = HttpMethod.Get,
            [HttpMethods.Head] = HttpMethod.Head,
            [HttpMethods.Post] = HttpMethod.Post,
            [HttpMethods.Put] = HttpMethod.Put,
            [HttpMethods.Delete] = HttpMethod.Delete,
            [HttpMethods.Connect] = new HttpMethod("CONNECT"),
            [HttpMethods.Options] = HttpMethod.Options,
            [HttpMethods.Trace] = new HttpMethod("TRACE"),
            [HttpMethods.Patch] = new HttpMethod("PATCH")
        };

    /// <summary>
    /// Given a bitmask of HttpMethods, return the matching HttpMethod instances.
    /// </summary>
    public static IEnumerable<HttpMethod> ToHttpMethods(this HttpMethods flags)
    {
        // iterate only non-zero defined flags
        foreach (var kvp in _map)
        {
            if (flags.HasFlag(kvp.Key))
                yield return kvp.Value;
        }
    }

    /// <summary>
    /// Filter an existing sequence of HttpMethod by the flags bitmask.
    /// </summary>
    public static IEnumerable<HttpMethod> FilterBy(this IEnumerable<HttpMethod> available, HttpMethods flags)
    {
        // turn flags → methods once, then intersect
        var allowed = new HashSet<HttpMethod>(flags.ToHttpMethods());
        return available.Where(allowed.Contains);
    }
}
