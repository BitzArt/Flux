using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public interface ICommunicatorServicePreBuilder
{
    internal string? Name { get; set; }
    internal IServiceCollection Services { get; }
    internal ICommunicatorServiceFactory Factory { get; }
}
