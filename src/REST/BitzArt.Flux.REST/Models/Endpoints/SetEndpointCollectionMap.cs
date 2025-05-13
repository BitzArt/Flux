namespace BitzArt.Flux.REST.Endpoints;

internal class SetEndpointCollectionMap
{
    private readonly Dictionary<Type, Dictionary<string, object>> _values;

    public SetEndpointCollectionMap()
    {
        _values = [];
    }

    public void Set<TRequirements>(string method, SetEndpointResolver<TRequirements> value)
        where TRequirements : SetEndpointRequirements
    {
        if (!_values.TryGetValue(typeof(TRequirements), out var dictionary))
        {
            dictionary = [];
            _values[typeof(TRequirements)] = dictionary;
        }

        dictionary[method] = value;
    }
}
