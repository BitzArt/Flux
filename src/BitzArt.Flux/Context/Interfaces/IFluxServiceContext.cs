namespace BitzArt.Flux;

public interface IFluxServiceContext
{
    public IFluxModelContext<TModel, TKey> Model<TModel, TKey>() where TModel : class;
    public IFluxModelContext<TModel> Model<TModel>() where TModel : class;
}