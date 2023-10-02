namespace BitzArt.Flux;

public interface IFluxServiceContext
{
    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>() where TModel : class;
    public IFluxSetContext<TModel> Set<TModel>() where TModel : class;
}