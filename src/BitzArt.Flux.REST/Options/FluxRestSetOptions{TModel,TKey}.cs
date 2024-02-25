namespace BitzArt.Flux;

public class FluxRestSetOptions<TModel, TKey> : FluxRestSetOptions<TModel>
    where TModel : class
{
    public new Func<TKey?, object[]?, string>? GetIdEndpointAction
    {
        get
        {
            if (_getIdEndpointAction is null) return null;
            return (key, parameters) => _getIdEndpointAction!(key!, parameters);
        }
        set
        {
            if (value is null)
            {
                _getIdEndpointAction = null;
                return;
            }
            _getIdEndpointAction = (key, parameters) => value!((TKey?)key, parameters);
        }
    }

    internal override Type? KeyType => typeof(TKey);

    public FluxRestSetOptions()
    {
        GetIdEndpointAction = null;
    }
}