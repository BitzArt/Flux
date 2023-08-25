namespace BitzArt.Communicator;

internal class CommunicatorBuilder : ICommunicatorBuilder
{
    public ICommunicatorServiceFactory Factory { get; init; }

	public CommunicatorBuilder()
	{
		Factory = new CommunicatorServiceFactory();
	}
}
