namespace BitzArt.Flux;

internal class FluxRestSetOptions<TModel, TKey> : FluxRestSetOptions<TModel>
    where TModel : class
{
    private Func<TKey?, object[]?, string>? _getIdEndpointAction;

    protected internal override Func<object?, object[]?, string>? GetIdEndpointActionInternal
    {
        get
        {
            if (_getIdEndpointAction is null) return null;
            return (key, parameters) =>
            {
                if (key is not null && key is not TKey) throw new InvalidOperationException("Invalid key type");
                return _getIdEndpointAction!((TKey?)key, parameters);
            };
        }
        set
        {
            _getIdEndpointAction = value is not null
                ? (key, parameters) => value?.Invoke(key, parameters)!
                : null;
        }
    }

    protected internal new Func<TKey?, object[]?, string>? GetIdEndpointAction
    {
        get => _getIdEndpointAction;
        set => _getIdEndpointAction = value;
    }

    internal Func<object?, object[]?, string>? BaseGetIdEndpointAction => base.GetIdEndpointAction;

    internal override Type? KeyType => typeof(TKey);

    public FluxRestSetOptions()
    {
        GetIdEndpointAction = null;
    }
}