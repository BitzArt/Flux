using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public interface ICommunicatorBuilder
{
    public ICommunicatorServiceFactory Factory { get; }
    public IServiceCollection Services { get; }
}
