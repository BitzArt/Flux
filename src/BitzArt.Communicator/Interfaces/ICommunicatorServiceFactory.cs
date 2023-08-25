using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public interface ICommunicatorServiceFactory
{
    public IServiceCollection Services { get; }
    public void AddService(ICommunicatorServiceBuilder builder);
}