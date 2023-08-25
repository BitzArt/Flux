namespace BitzArt.Communicator;

public interface ICommunicatorServicePreBuilder
{
    public string? Name { get; }
    public ICommunicatorServiceFactory Factory { get; }
}
