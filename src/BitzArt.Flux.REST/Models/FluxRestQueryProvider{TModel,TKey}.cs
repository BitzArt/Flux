using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxRestQueryProvider<TModel, TKey>(
    FluxRestSetContext<TModel, TKey> setContext
    ) : FluxRestQueryProvider<TModel>(setContext)
    where TModel : class
{
    protected override FluxRestHttpRequestBuilder<TModel> CreateRequestBuilder()
    {
        return new FluxRestHttpRequestBuilder<TModel, TKey>((SetContext as FluxRestSetContext<TModel, TKey>)!);
    }

    public override IQueryable<TResult> CreateQuery<TResult>(Expression expression)
    {
        if (typeof(TResult) != typeof(TModel))
            throw new NotSupportedException("Result type not supported");

        return (new FluxRestQueryable<TModel, TKey>(this, expression) as IQueryable<TResult>)!;
    }
}
