using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

internal class CommunicatorServiceFactory : ICommunicatorServiceFactory
{
    private IDictionary<string, ICommunicatorServiceProvider> _providers;

    public IServiceCollection Services { get; }

    public CommunicatorServiceFactory(IServiceCollection services)
    {
        Services = services;
        _providers = new Dictionary<string, ICommunicatorServiceProvider>();
    }

    public void AddService(ICommunicatorServiceBuilder builder)
    {
        throw new NotImplementedException();
    }
}