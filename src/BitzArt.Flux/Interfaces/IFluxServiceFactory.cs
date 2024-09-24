namespace BitzArt.Flux;

/// <summary>
/// Internal Flux Service factory.
/// </summary>
public interface IFluxServiceFactory
{
    public string ServiceName { get; }

    public IFluxSetContext<TModel> CreateSetContext<TModel>(IServiceProvider services, string? name = null) where TModel : class;
    public IFluxSetContext<TModel, TKey> CreateSetContext<TModel, TKey>(IServiceProvider services, string? name = null) where TModel : class;

    public void AddSet<TModel>(object options, string? name) where TModel : class;
    public void AddSet<TModel, TKey>(object options, string? name) where TModel : class;

    public bool ContainsSignature<TModel>(string? setName);
    public bool ContainsSignature<TModel, TKey>(string? setName);
}