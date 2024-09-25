using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxJsonSetBuilder<TModel, TKey> : IFluxJsonSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxJsonServiceBuilder ServiceBuilder { get; init; }

    public IServiceCollection Services => ServiceBuilder.Services;
    public IFluxServiceFactory ServiceFactory => ServiceBuilder.ServiceFactory;
    public IFluxFactory Factory => ServiceBuilder.Factory;
    public FluxJsonServiceOptions ServiceOptions => ServiceBuilder.ServiceOptions;
    public string? BaseFilePath => ServiceBuilder.ServiceOptions.BaseFilePath;

    public FluxJsonSetOptions<TModel, TKey> SetOptions { get; set; }

    IFluxJsonSetOptions<TModel> IFluxJsonSetBuilder<TModel, TKey>.SetOptions => SetOptions;

    public FluxJsonSetBuilder(IFluxJsonServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
        SetOptions = new FluxJsonSetOptions<TModel, TKey>();
    }
}