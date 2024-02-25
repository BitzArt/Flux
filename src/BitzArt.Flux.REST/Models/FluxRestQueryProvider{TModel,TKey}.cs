using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxRestQueryProvider<TModel, TKey> : FluxRestQueryProvider<TModel>
    where TModel : class
{
    public FluxRestQueryProvider(FluxRestSetContext<TModel, TKey> setContext) : base(setContext) { }

    public override IQueryable<TResult> CreateQuery<TResult>(Expression expression)
    {
        if (typeof(TResult) != typeof(TModel))
            throw new NotSupportedException("Result type not supported");

        if (SetContext is not FluxRestSetContext<TModel, TKey> setContextCasted)
            throw new NotSupportedException("Set context not supported");

        return (new FluxRestQueryable<TModel, TKey>(setContextCasted) as IQueryable<TResult>)!;
    }
}
