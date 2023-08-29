namespace BitzArt.Communicator;

public class CommunicatorRestEntityOptions
{
	public Func<string>? GetEntityEndpointAction { get; set; }

	public CommunicatorRestEntityOptions()
	{
		GetEntityEndpointAction = null;
	}
}