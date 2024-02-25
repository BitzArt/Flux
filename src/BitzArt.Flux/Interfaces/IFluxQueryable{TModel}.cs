namespace BitzArt.Flux;

public interface IFluxQueryable<TModel> : IQueryable<TModel>
    where TModel : class
{
    public Task<TModel> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
}