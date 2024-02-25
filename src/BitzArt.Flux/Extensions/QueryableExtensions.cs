namespace BitzArt.Flux;

public static class QueryableExtensions
{
    public static async Task<TModel> FirstOrDefaultAsync<TModel>(this IQueryable<TModel> source)
        where TModel : class
    {
        if (source is not IFluxQueryable<TModel> fluxRestQueryable)
            throw new InvalidOperationException(nameof(FirstOrDefaultAsync));

        return await fluxRestQueryable.FirstOrDefaultAsync();
    }
}
