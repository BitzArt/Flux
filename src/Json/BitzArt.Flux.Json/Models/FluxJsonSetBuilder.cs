using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxJsonSetBuilder<TModel, TKey>(IFluxJsonServiceBuilder serviceBuilder
    ) : IFluxJsonSetBuilder<TModel, TKey>
    where TModel : class
{
    public IFluxJsonServiceBuilder ServiceBuilder { get; init; } = serviceBuilder;

    public IServiceCollection Services => ServiceBuilder.Services;
    public IFluxServiceFactory ServiceFactory => ServiceBuilder.ServiceFactory;
    public IFluxFactory Factory => ServiceBuilder.Factory;
    public FluxJsonServiceOptions ServiceOptions => ServiceBuilder.ServiceOptions;
    public string? BaseFilePath => ServiceBuilder.ServiceOptions.BaseFilePath;

    public FluxJsonSetOptions<TModel, TKey> SetOptions { get; set; } = new FluxJsonSetOptions<TModel, TKey>();

    IFluxJsonSetOptions<TModel> IFluxJsonSetBuilder<TModel, TKey>.SetOptions => SetOptions;
}