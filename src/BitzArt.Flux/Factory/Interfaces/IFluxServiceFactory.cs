namespace BitzArt.Flux;

public interface IFluxServiceFactory
{
    internal string ServiceName { get; }

    internal IFluxSetContext<TModel> CreateSetContext<TModel>(IServiceProvider services) where TModel : class;
    internal IFluxSetContext<TModel, TKey> CreateSetContext<TModel, TKey>(IServiceProvider services) where TModel : class;
    internal void AddSet<TModel>(object options) where TModel : class;
    internal void AddSet<TModel, TKey>(object options) where TModel : class;

    internal bool ContainsSignature<TModel>();
    internal bool ContainsSignature<TModel, TKey>();
}