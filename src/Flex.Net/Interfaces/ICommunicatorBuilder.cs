using Microsoft.Extensions.DependencyInjection;

namespace Flex;

public interface ICommunicatorBuilder
{
    internal ICommunicatorServiceFactory Factory { get; }
    internal IServiceCollection Services { get; }
}
