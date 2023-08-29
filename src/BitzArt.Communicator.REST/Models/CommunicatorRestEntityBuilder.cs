using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

internal class CommunicatorRestEntityBuilder : ICommunicatorRestEntityBuilder, ICommunicatorRestServiceBuilder
{
    public ICommunicatorRestServiceBuilder ServiceBuilder { get; init; }

    public IServiceCollection Services => ServiceBuilder.Services;
    public ICommunicatorServiceProvider Provider => ServiceBuilder.Provider;
    public ICommunicatorServiceFactory Factory => ServiceBuilder.Factory;
    public CommunicatorRestServiceOptions ServiceOptions => ServiceBuilder.ServiceOptions;

    public Action<HttpClient>? HttpClientConfiguration
    {
        get => ServiceBuilder.HttpClientConfiguration;
        set => ServiceBuilder.HttpClientConfiguration = value;
    }

    public CommunicatorRestEntityOptions EntityOptions { get; set; }

    public CommunicatorRestEntityBuilder(ICommunicatorRestServiceBuilder serviceBuilder)
	{
		ServiceBuilder = serviceBuilder;
        EntityOptions = new();
	}
}
