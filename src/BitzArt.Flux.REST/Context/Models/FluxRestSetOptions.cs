namespace BitzArt.Flux;

public class FluxRestSetOptions<TModel>
    where TModel : class
{
    public string? Endpoint { get; set; }
    public string? PageEndpoint { get; set; }
    protected Func<object?, object[]?, string>? _getIdEndpointAction;
    public Func<object?, object[]?, string>? GetIdEndpointAction
    {
        get => _getIdEndpointAction;
        set => _getIdEndpointAction = value;
    }

    public FluxRestSetOptions()
    {
        GetIdEndpointAction = null;
    }
}

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

    public FluxRestSetOptions()
    {
        GetIdEndpointAction = null;
    }
}