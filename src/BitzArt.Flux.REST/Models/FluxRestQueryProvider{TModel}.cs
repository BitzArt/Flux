using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxRestQueryProvider<TModel> : IQueryProvider
        where TModel : class
{
    protected readonly FluxRestSetContext<TModel> SetContext;

    public FluxRestQueryProvider(FluxRestSetContext<TModel> setContext)
    {
        SetContext = setContext ?? throw new ArgumentNullException(nameof(setContext));
    }

    public IQueryable CreateQuery(Expression expression)
    {
        throw new NotImplementedException();
    }

    public virtual IQueryable<TResult> CreateQuery<TResult>(Expression expression)
    {
        if (typeof(TResult) != typeof(TModel))
            throw new NotSupportedException("Result type not supported");

        return (new FluxRestQueryable<TModel>(SetContext) as IQueryable<TResult>)!;
    }

    public object Execute(Expression expression)
    {
        throw new NotImplementedException();
    }

    public TResult Execute<TResult>(Expression expression)
    {
        if (typeof(TResult) != typeof(IEnumerable<TModel>) && typeof(TResult) != typeof(Task<TModel>))
            throw new NotSupportedException("Result type not supported");

        // You need to interpret the expression and perform the corresponding operation here.
        throw new NotImplementedException();
    }
}
