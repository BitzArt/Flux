using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxRestSetBuilder<TModel> : IFluxRestSetBuilder<TModel>
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

    public FluxRestSetOptions<TModel> SetOptions { get; set; }

    public FluxRestSetBuilder(IFluxRestServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
        SetOptions = new();
    }
}

internal class FluxRestSetBuilder<TModel, TKey> : FluxRestSetBuilder<TModel>, IFluxRestSetBuilder<TModel, TKey>
    where TModel : class
{
    public new FluxRestSetOptions<TModel, TKey> SetOptions { get; set; }

    public FluxRestSetBuilder(IFluxRestServiceBuilder serviceBuilder) : base(serviceBuilder)
    {
        SetOptions = new();
    }
}
