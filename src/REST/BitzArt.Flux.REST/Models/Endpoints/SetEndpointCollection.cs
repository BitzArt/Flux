using System.Reflection;

namespace BitzArt.Flux.REST.Endpoints;

internal class SetEndpointCollection<TModel, TKey>
    where TModel : class
{
    private readonly SetConfiguration<TModel, TKey> _setConfiguration;

    private readonly SetEndpointCollectionMap _map;

    public SetEndpointCollection(SetConfiguration<TModel, TKey> setConfiguration)
    {
        _setConfiguration = setConfiguration;
        _map = new();
    }

    public void Add<TRequirements>(SetEndpointResolver<TRequirements> resolver, HttpMethods httpMethods)
        where TRequirements : SetEndpointRequirements
    {
        var methods = Enum.GetValues<HttpMethods>()
            .Cast<HttpMethods>()
            .Where(m => m != 0 && ((int)m & ((int)m - 1)) == 0) // filter out composite enum values
            .Select(m => m.ToString().ToUpperInvariant())
            .ToArray();

        foreach (var method in methods)
        {
            _map.Set(method, resolver);
        }
    }

    public HttpRequestMessage Resolve(OperationDescriptor descriptor)
    {
        var endpointRequirements = GetEndpointRequirements(descriptor);

        var genericMethod = typeof(SetEndpointCollection<,>)
            .GetMethod(nameof(Resolve), BindingFlags.NonPublic | BindingFlags.Instance)
            ?.MakeGenericMethod(endpointRequirements.GetType());

        return (HttpRequestMessage)genericMethod!.Invoke(this, new object[] { endpointRequirements });
    }

    private HttpRequestMessage Resolve<TRequirements>(SetEndpointRequirements requirements)
        where TRequirements : SetEndpointRequirements
    {
        var resolver = GetResolver<TRequirements>(requirements.HttpMethod);

        
    }

    private SetEndpointResolver<TRequirements> GetResolver<TRequirements>(HttpMethod httpMethod)
        where TRequirements : SetEndpointRequirements
    {
        var result = _map.Get<TRequirements>(httpMethod);

        if (result is null)
        {
            var stepDownResolver = GetStepDownResolver<TRequirements>(HttpMethod httpMethod)
                ?? throw new InvalidOperationException($"No resolver found for {typeof(TRequirements).Name} with HTTP method {httpMethod}");

            return GetResolverFromStepDown
        }
    }
}
