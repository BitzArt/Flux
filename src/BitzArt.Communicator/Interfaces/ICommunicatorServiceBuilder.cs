using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public interface ICommunicatorServiceBuilder
{
    public IServiceCollection Services { get; }
    public ICommunicatorServiceProvider Provider { get; }
    public ICommunicatorServiceFactory Factory { get; }
}