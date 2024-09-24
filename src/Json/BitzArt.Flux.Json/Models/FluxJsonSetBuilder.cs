using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal class FluxJsonSetBuilder<TModel> : IFluxJsonSetBuilder<TModel>
    where TModel : class
{
    public IFluxJsonServiceBuilder ServiceBuilder { get; init; }

    public IServiceCollection Services => ServiceBuilder.Services;
    public IFluxServiceFactory ServiceFactory => ServiceBuilder.ServiceFactory;
    public IFluxFactory Factory => ServiceBuilder.Factory;
    public FluxJsonServiceOptions ServiceOptions => ServiceBuilder.ServiceOptions;
    public string? BaseFilePath => ServiceBuilder.ServiceOptions.BaseFilePath;
    protected FluxJsonSetOptions<TModel> _setOptions = null!;
    
    public FluxJsonSetOptions<TModel> SetOptions { get => _setOptions; set => _setOptions = value; }
    
    public FluxJsonSetBuilder(IFluxJsonServiceBuilder serviceBuilder)
    {
        ServiceBuilder = serviceBuilder;
        SetOptions = new FluxJsonSetOptions<TModel>();
    }
}

internal class FluxJsonSetBuilder<TModel, TKey> : FluxJsonSetBuilder<TModel>, IFluxJsonSetBuilder<TModel, TKey>
    where TModel : class
{
    public new FluxJsonSetOptions<TModel, TKey> SetOptions
    {
        get => (FluxJsonSetOptions<TModel, TKey>)_setOptions;
        set => _setOptions = value;
    }

    public FluxJsonSetBuilder(IFluxJsonServiceBuilder serviceBuilder) : base(serviceBuilder)
    {
        SetOptions = new FluxJsonSetOptions<TModel, TKey>();
    }
}