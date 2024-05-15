using System.Text;

namespace BitzArt.Flux;

public static class QueryExtensions
{
    public static void Add(this ICollection<KeyValuePair<string, string>> query, string key, string value)
        => query.Add(new(key, value));

    public static string ToQueryString(this ICollection<KeyValuePair<string, string>> query)
    {
        if (query is null || query.Count == 0) return string.Empty;

        var sb = new StringBuilder();
        sb.Append('?');

        var first = true;

        foreach (var kvp in query)
        {
            if (!first) sb.Append('&');

            sb.Append(kvp.Key);
            sb.Append('=');
            sb.Append(kvp.Value);

            first = false;
        }

        return sb.ToString();
    }
}
