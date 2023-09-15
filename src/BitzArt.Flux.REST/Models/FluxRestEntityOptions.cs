namespace BitzArt.Flux;

public class FluxRestEntityOptions<TEntity>
	where TEntity : class
{
	public string? Endpoint { get; set; }
	protected Func<object?, object[]?, string>? _getIdEndpointAction;
    public Func<object?, object[]?, string>? GetIdEndpointAction
	{
		get => _getIdEndpointAction;
		set => _getIdEndpointAction = value;
	}

    public FluxRestEntityOptions()
	{
		GetIdEndpointAction = null;
	}
}

public class FluxRestEntityOptions<TEntity, TKey> : FluxRestEntityOptions<TEntity>
    where TEntity : class
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

    public FluxRestEntityOptions()
	{
		GetIdEndpointAction = null;
    }
}