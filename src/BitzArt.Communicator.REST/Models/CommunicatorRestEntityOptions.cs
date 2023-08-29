namespace BitzArt.Communicator;

public class CommunicatorRestEntityOptions<TEntity>
	where TEntity : class
{
	public string? Endpoint { get; set; }
    public Func<object, string>? GetIdEndpointAction { get; set; }

    public CommunicatorRestEntityOptions()
	{
		GetIdEndpointAction = null;
	}
}

public class CommunicatorRestEntityOptions<TEntity, TKey> : CommunicatorRestEntityOptions<TEntity>
    where TEntity : class
{
	public new Func<TKey, string>? GetIdEndpointAction { get; set; }

	public CommunicatorRestEntityOptions()
	{
		GetIdEndpointAction = null;
    }
}