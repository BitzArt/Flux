namespace BitzArt.Communicator;

public class CommunicatorRestEntityOptions<TEntity>
	where TEntity : class
{
	public string? Endpoint { get; set; }
	protected Func<object?, string>? _getIdEndpointAction;
    public Func<object?, string>? GetIdEndpointAction
	{
		get => _getIdEndpointAction;
		set => _getIdEndpointAction = value;
	}

    public CommunicatorRestEntityOptions()
	{
		GetIdEndpointAction = null;
	}
}

public class CommunicatorRestEntityOptions<TEntity, TKey> : CommunicatorRestEntityOptions<TEntity>
    where TEntity : class
{
	public new Func<TKey?, string>? GetIdEndpointAction
	{
		get
		{
			if (_getIdEndpointAction is null) return null;
			return (key) => _getIdEndpointAction!(key!);
		}
		set
		{
			if (value is null)
			{
                _getIdEndpointAction = null;
				return;
            }
            _getIdEndpointAction = (key) => value!((TKey?)key);
		}
    }

    public CommunicatorRestEntityOptions()
	{
		GetIdEndpointAction = null;
    }
}