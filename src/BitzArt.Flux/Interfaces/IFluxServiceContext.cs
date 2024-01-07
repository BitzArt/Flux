namespace BitzArt.Flux;

/// <summary>
/// Flux context for a specific configured Service.
/// See <see href="https://bitzart.github.io/Flux/03.use.html">Use Flux</see> for more information.
/// </summary>
public interface IFluxServiceContext
{
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? name = null) where TModel : class;
    public IFluxSetContext<TModel> Set<TModel>(string? name = null) where TModel : class;
}