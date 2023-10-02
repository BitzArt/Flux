namespace BitzArt.Flux;

public interface IFluxContext
{
    public IFluxServiceContext Service(string serviceName);

    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? serviceName = null) where TModel : class;
    public IFluxSetContext<TModel> Set<TModel>(string? serviceName = null) where TModel : class;
}