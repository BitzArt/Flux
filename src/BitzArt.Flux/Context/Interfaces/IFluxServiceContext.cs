namespace BitzArt.Flux;

public interface IFluxServiceContext
{
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? name = null) where TModel : class;
    public IFluxSetContext<TModel> Set<TModel>(string? name = null) where TModel : class;
}