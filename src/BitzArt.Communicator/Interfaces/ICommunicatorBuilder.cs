namespace BitzArt.Communicator;

public interface ICommunicatorBuilder
{
    public ICommunicatorServiceFactory Factory { get; init; }
}
