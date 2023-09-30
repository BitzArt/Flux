namespace BitzArt.Flux;

internal interface IFluxFactory
{
    internal ICollection<IFluxServiceFactory> ServiceContexts { get; }

    internal IFluxServiceFactory GetServiceProvider(string name);

    internal IFluxModelContext<TModel> GetModelContext<TModel>(IServiceProvider services, string? serviceName = null)
        where TModel : class;

    internal IFluxModelContext<TModel, TKey> GetModelContext<TModel, TKey>(IServiceProvider services, string? serviceName = null)
        where TModel : class;
}