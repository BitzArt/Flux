namespace BitzArt.Flux.REST;

internal static class HttpMethodExtensions
{
    public static bool IsSubsetOf(this IEnumerable<HttpMethod> httpMethods, IEnumerable<HttpMethod> other)
    {
        if (httpMethods.Any(x => !other.Contains(x)))
        {
            return false;
        }

        return true;
    }
}
