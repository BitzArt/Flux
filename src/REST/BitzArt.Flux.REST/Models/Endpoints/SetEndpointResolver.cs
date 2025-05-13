namespace BitzArt.Flux.REST.Endpoints;

internal class SetEndpointResolver<TRequirements>
    where TRequirements : SetEndpointRequirements
{
    private readonly Func<TRequirements, HttpRequestMessage> _resolver;

    public SetEndpointResolver(Func<TRequirements, string> pathResolver)
    {
        _resolver = (requirements) =>
        {
            var httpMethod = requirements.HttpMethod;
            var path = pathResolver.Invoke(requirements);

            return Resolve(httpMethod, path);
        };
    }

    public SetEndpointResolver(Func<TRequirements, HttpRequestMessage> resolver)
    {
        ArgumentNullException.ThrowIfNull(resolver);
        _resolver = resolver;
    }

    public virtual HttpRequestMessage Resolve(TRequirements requirements) => _resolver.Invoke(requirements);

    private static HttpRequestMessage Resolve(HttpMethod httpMethod, string path)
    {
        var requestMessage = new HttpRequestMessage(httpMethod, path);
        requestMessage.Headers.Accept.Add(new("application/json"));
        return requestMessage;
    }
}
