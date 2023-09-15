using Microsoft.Extensions.DependencyInjection;

namespace Flex;

public interface ICommunicatorServiceBuilder
{
    internal IServiceCollection Services { get; }
    internal ICommunicatorServiceProvider Provider { get; }
    internal ICommunicatorServiceFactory Factory { get; }
}