using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

public interface ICommunicatorServicePreBuilder
{
    public string? Name { get; set; }
    public IServiceCollection Services { get; }
    public ICommunicatorServiceFactory Factory { get; }
}
