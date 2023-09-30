using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxRestModelBuilder<TModel> : IFluxRestModelBuilder<TModel>
    where TModel : class
{
    public IFluxRestServiceBuilder ServiceBuilder { get; init; }

    public IServiceCollection Services => ServiceBuilder.Services;
    public IFluxServiceFactory ServiceFactory => ServiceBuilder.ServiceFactory;
    public IFluxFactory Factory => ServiceBuilder.Factory;
    public FluxRestServiceOptions ServiceOptions => ServiceBuilder.ServiceOptions;

    public Action<HttpClient>? HttpClientConfiguration
    {
        get => ServiceBuilder.HttpClientConfiguration;
        set => ServiceBuilder.HttpClientConfiguration = value;
    }

    public FluxRestModelOptions<TModel> ModelOptions { get; set; }

    public FluxRestModelBuilder(IFluxRestServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
        ModelOptions = new();
    }
}

internal class FluxRestModelBuilder<TModel, TKey> : FluxRestModelBuilder<TModel>, IFluxRestModelBuilder<TModel, TKey>
    where TModel : class
{
    public new FluxRestModelOptions<TModel, TKey> ModelOptions { get; set; }

    public FluxRestModelBuilder(IFluxRestServiceBuilder serviceBuilder) : base(serviceBuilder)
    {
        ModelOptions = new();
    }
}
