using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public interface ICommunicatorBuilder
{
    internal ICommunicatorServiceFactory Factory { get; }
    internal IServiceCollection Services { get; }
}
