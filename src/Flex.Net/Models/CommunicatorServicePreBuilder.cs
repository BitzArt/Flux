using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

internal class CommunicatorServicePreBuilder : ICommunicatorServicePreBuilder
{
    public string? Name { get; set; }
    public IServiceCollection Services { get; init; }
    public ICommunicatorServiceFactory Factory { get; init; }

    public CommunicatorServicePreBuilder(IServiceCollection services, ICommunicatorServiceFactory factory, string? name)
    {
        Services = services;
        Factory = factory;
        Name = name;
    }
}
