namespace Flex;

public class CommunicatorRestEntityOptions<TEntity>
	where TEntity : class
{
	public string? Endpoint { get; set; }
	protected Func<object?, object[]?, string>? _getIdEndpointAction;
    public Func<object?, object[]?, string>? GetIdEndpointAction
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

    public CommunicatorRestEntityOptions()
	{
		GetIdEndpointAction = null;
    }
}