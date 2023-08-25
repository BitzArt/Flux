namespace BitzArt.Communicator;

public interface ICommunicatorServiceProvider
{
    public IEnumerable<IEntityCommunicator> EntityCommunicators { get; }
}