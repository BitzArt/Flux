namespace BitzArt.Communicator;

public interface ICommunicatorRestEntityBuilder : ICommunicatorEntityBuilder
{
    public CommunicatorRestEntityOptions EntityOptions { get; }
}
