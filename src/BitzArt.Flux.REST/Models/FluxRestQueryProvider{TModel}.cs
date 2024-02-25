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
        if (expression is null) throw new ArgumentNullException(nameof(expression));

        var request = Translate(expression);
        return await SetContext.HandleRequestAsync<TResult>(request, cancellationToken);
    }

    private HttpRequestMessage Translate(Expression expression)
    {
        // Check if the expression represents a simple filter
        if (expression is LambdaExpression lambda && lambda.Body is BinaryExpression binary && lambda.Parameters.Count == 1)
        {
            if (binary.NodeType == ExpressionType.Equal && binary.Left is MemberExpression member && binary.Right is ConstantExpression constant)
            {
                // Assuming member represents the property to filter on (e.g., x.Id) and constant represents the value to filter for (e.g., 1)
                var propertyName = member.Member.Name;
                var propertyValue = constant.Value;

                // Construct the HTTP request to fetch the item with the specified property value
                var endpoint = $"{SetContext.SetOptions.Endpoint}/{propertyValue}";
                return new HttpRequestMessage(HttpMethod.Get, endpoint);
            }
        }

        // If the expression is not a simple filter, throw an exception or handle it appropriately
        throw new NotSupportedException("Unsupported LINQ expression");
    }
}
