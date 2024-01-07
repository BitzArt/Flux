namespace BitzArt.Flux;

/// <summary>
/// Internal Flux Service factory.
/// </summary>
public interface IFluxServiceFactory
{
    internal string ServiceName { get; }

    internal IFluxSetContext<TModel> CreateSetContext<TModel>(IServiceProvider services, string? name = null) where TModel : class;
    internal IFluxSetContext<TModel, TKey> CreateSetContext<TModel, TKey>(IServiceProvider services, string? name = null) where TModel : class;
    internal void AddSet<TModel>(object options, string? name) where TModel : class;
    internal void AddSet<TModel, TKey>(object options, string? name) where TModel : class;

    internal bool ContainsSignature<TModel>(string? setName);
    internal bool ContainsSignature<TModel, TKey>(string? setName);
}