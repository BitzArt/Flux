namespace BitzArt.Flux;

public interface IFluxContext
{
    public IFluxServiceContext Service(string serviceName);

    public IFluxModelContext<TModel, TKey> Model<TModel, TKey>(string? serviceName = null) where TModel : class;
    public IFluxModelContext<TModel> Model<TModel>(string? serviceName = null) where TModel : class;
}