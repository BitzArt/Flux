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

    public Action<IServiceProvider, HttpClient>? HttpClientConfiguration
    {
        get => ServiceBuilder.HttpClientConfiguration;
        set => ServiceBuilder.HttpClientConfiguration = value;
    }

    public Func<IServiceProvider, DelegatingHandler>? ConfigureHandler
    {
        get => ServiceBuilder.ConfigureHandler;
        set => ServiceBuilder.ConfigureHandler = value;
    }

    protected FluxRestSetOptions<TModel> _setOptions = null!;
    
    public FluxRestSetOptions<TModel> SetOptions { get => _setOptions; set => _setOptions = value; }

    public FluxRestSetBuilder(IFluxRestServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
        SetOptions = new();
    }
}
