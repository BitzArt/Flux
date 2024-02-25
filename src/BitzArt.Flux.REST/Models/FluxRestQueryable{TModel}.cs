using System.Collections;
using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxRestQueryable<TModel> : IQueryable<TModel>
    where TModel : class
{
    protected readonly FluxRestSetContext<TModel> SetContext;

    public FluxRestQueryable(FluxRestSetContext<TModel> setContext)
    {
        SetContext = setContext ?? throw new ArgumentNullException(nameof(setContext));
    }

    public Type ElementType => typeof(TModel);

    public Expression Expression => Expression.Constant(this);

    public IQueryProvider Provider => new FluxRestQueryProvider<TModel>(SetContext);

    public IEnumerator<TModel> GetEnumerator() => throw new EnumerationNotSupportedException();

    IEnumerator IEnumerable.GetEnumerator() => throw new EnumerationNotSupportedException();
}
