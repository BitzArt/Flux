namespace BitzArt.Flux;

public interface IFluxContext
{
    public IFluxServiceContext Service(string serviceName);

    public IFluxSetContext<TModel, TKey> Set<TModel, TKey>(string? service = null, string? set = null) where TModel : class;
    public IFluxSetContext<TModel> Set<TModel>(string? service = null, string? set = null) where TModel : class;
}