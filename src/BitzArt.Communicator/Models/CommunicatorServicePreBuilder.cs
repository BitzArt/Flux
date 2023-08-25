namespace BitzArt.Communicator;

internal class CommunicatorServicePreBuilder : ICommunicatorServicePreBuilder
{
    public string? Name { get; internal set; }
    public ICommunicatorServiceFactory Factory { get; init; }

    public CommunicatorServicePreBuilder(ICommunicatorServiceFactory factory, string? name)
    {
        Factory = factory;
        Name = name;
    }
}
