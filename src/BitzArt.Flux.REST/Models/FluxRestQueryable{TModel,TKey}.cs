using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxRestQueryable<TModel, TKey> : FluxRestQueryable<TModel>
    where TModel : class
{
    public FluxRestQueryable(
        FluxRestQueryProvider<TModel, TKey> provider,
        Expression expression
        ) : base(provider, expression) { }
}
