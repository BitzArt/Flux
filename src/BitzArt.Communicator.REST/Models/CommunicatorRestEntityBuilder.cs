using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator;

internal class CommunicatorRestEntityBuilder<TEntity> : ICommunicatorRestEntityBuilder<TEntity>
    where TEntity : class
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

    public CommunicatorRestEntityOptions<TEntity> EntityOptions { get; set; }

    public CommunicatorRestEntityBuilder(ICommunicatorRestServiceBuilder serviceBuilder)
	{
		ServiceBuilder = serviceBuilder;
        EntityOptions = new();
	}
}

internal class CommunicatorRestEntityBuilder<TEntity, TKey> : CommunicatorRestEntityBuilder<TEntity>, ICommunicatorRestEntityBuilder<TEntity, TKey>
    where TEntity : class
{
    public new CommunicatorRestEntityOptions<TEntity, TKey> EntityOptions { get; set; }

    public CommunicatorRestEntityBuilder(ICommunicatorRestServiceBuilder serviceBuilder) : base(serviceBuilder)
    {
        EntityOptions = new();
    }
}
