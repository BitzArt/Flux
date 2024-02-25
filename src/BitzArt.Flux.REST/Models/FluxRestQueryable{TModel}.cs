using System.Collections;
using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxRestQueryable<TModel>(FluxRestQueryProvider<TModel> provider, Expression expression) : IFluxQueryable<TModel>
    where TModel : class
{
    protected readonly Expression _expression = expression ?? throw new ArgumentNullException(nameof(expression));
    protected readonly FluxRestQueryProvider<TModel> _provider = provider ?? throw new ArgumentNullException(nameof(provider));

    public Type ElementType => typeof(TModel);

    public Expression Expression => _expression;

    public IQueryProvider Provider => _provider;

    public IEnumerator<TModel> GetEnumerator() => throw new EnumerationNotSupportedException();

    IEnumerator IEnumerable.GetEnumerator() => throw new EnumerationNotSupportedException();

    public async Task<TModel> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        return await _provider.FirstOrDefaultAsync<TModel>(_expression, cancellationToken);
    }
}
