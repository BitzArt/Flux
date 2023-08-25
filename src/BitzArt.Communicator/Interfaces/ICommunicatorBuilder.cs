using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public interface ICommunicatorBuilder
{
    public ICommunicatorServiceFactory Factory { get; init; }
    IServiceCollection Services { get; }
}
