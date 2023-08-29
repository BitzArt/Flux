using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public interface ICommunicatorServiceBuilder
{
    internal IServiceCollection Services { get; }
    internal ICommunicatorServiceProvider Provider { get; }
    internal ICommunicatorServiceFactory Factory { get; }
}