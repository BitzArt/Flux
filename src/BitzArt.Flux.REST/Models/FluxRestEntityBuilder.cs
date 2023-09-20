using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxRestEntityBuilder<TEntity> : IFluxRestEntityBuilder<TEntity>
    where TEntity : class
{
    public IFluxRestServiceBuilder ServiceBuilder { get; init; }

    public IServiceCollection Services => ServiceBuilder.Services;
    public IFluxServiceProvider ServiceContext => ServiceBuilder.ServiceContext;
    public IFluxProvider Factory => ServiceBuilder.Factory;
    public FluxRestServiceOptions ServiceOptions => ServiceBuilder.ServiceOptions;

    public Action<HttpClient>? HttpClientConfiguration
    {
        get => ServiceBuilder.HttpClientConfiguration;
        set => ServiceBuilder.HttpClientConfiguration = value;
    }

    public FluxRestEntityOptions<TEntity> EntityOptions { get; set; }

    public FluxRestEntityBuilder(IFluxRestServiceBuilder serviceBuilder)
	{
		ServiceBuilder = serviceBuilder;
        EntityOptions = new();
	}
}

internal class FluxRestEntityBuilder<TEntity, TKey> : FluxRestEntityBuilder<TEntity>, IFluxRestEntityBuilder<TEntity, TKey>
    where TEntity : class
{
    public new FluxRestEntityOptions<TEntity, TKey> EntityOptions { get; set; }

    public FluxRestEntityBuilder(IFluxRestServiceBuilder serviceBuilder) : base(serviceBuilder)
    {
        EntityOptions = new();
    }
}
