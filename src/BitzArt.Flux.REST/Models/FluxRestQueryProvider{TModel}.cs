using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxRestQueryProvider<TModel> : IQueryProvider
        where TModel : class
{
    protected readonly FluxRestSetContext<TModel> SetContext;
    protected readonly FluxRestHttpRequestBuilder<TModel> RequestBuilder;

    public FluxRestQueryProvider(FluxRestSetContext<TModel> setContext)
    {
        SetContext = setContext ?? throw new ArgumentNullException(nameof(setContext));
        RequestBuilder = CreateRequestBuilder();
    }

    protected virtual FluxRestHttpRequestBuilder<TModel> CreateRequestBuilder()
    {
        return new FluxRestHttpRequestBuilder<TModel>(SetContext);
    }

    public IQueryable CreateQuery(Expression expression)
    {
        throw new NotImplementedException();
    }

    public virtual IQueryable<TResult> CreateQuery<TResult>(Expression expression)
    {
        if (typeof(TResult) != typeof(TModel))
            throw new NotSupportedException("Result type not supported");

        return (new FluxRestQueryable<TModel>(this, expression) as IQueryable<TResult>)!;
    }

    public object Execute(Expression expression)
    {
        throw new NotSupportedException();
    }

    public TResult Execute<TResult>(Expression expression)
    {
        throw new NotSupportedException();
    }

    public async Task<TResult> FirstOrDefaultAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(expression);

        var request = Translate(expression);

        return await SetContext.HandleRequestAsync<TResult>(request, cancellationToken);

    }

    private HttpRequestMessage Translate(Expression expression)
    {
        return RequestBuilder.Build(expression);
    }
}
