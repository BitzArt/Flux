namespace BitzArt.Flux;

public interface IFluxQueryable<TModel, TKey> : IFluxQueryable<TModel>
    where TModel : class
{
}