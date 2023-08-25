namespace BitzArt.Communicator;

internal class RestCommunicatorServiceProvider : ICommunicatorServiceProvider
{
    public IEnumerable<IEntityCommunicator> EntityCommunicators { get; init; }

    public RestCommunicatorServiceProvider()
    {
        EntityCommunicators = new HashSet<IEntityCommunicator>();
    }
}