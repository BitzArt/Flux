namespace BitzArt.Flux;

public interface IFluxServiceFactory
{
    internal string ServiceName { get; }

    internal IFluxModelContext<TModel> CreateModelContext<TModel>(IServiceProvider services) where TModel : class;
    internal IFluxModelContext<TModel, TKey> CreateModelContext<TModel, TKey>(IServiceProvider services) where TModel : class;
    internal void AddModel<TModel>(object options) where TModel : class;
    internal void AddModel<TModel, TKey>(object options) where TModel : class;

    internal bool ContainsSignature<TModel>();
    internal bool ContainsSignature<TModel, TKey>();
}