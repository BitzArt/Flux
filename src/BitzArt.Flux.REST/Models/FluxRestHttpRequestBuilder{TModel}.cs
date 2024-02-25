using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxRestHttpRequestBuilder<TModel>(FluxRestSetContext<TModel> set)
    where TModel : class
{

    public HttpRequestMessage Build(Expression expression)
    {
        if (expression is MethodCallExpression methodCallExpression && methodCallExpression.Method.Name == "Where")
            if (methodCallExpression.Arguments[1] is UnaryExpression unary && unary.Operand is LambdaExpression lambda)
                if (lambda.Body is BinaryExpression binary)
                    if (binary.Right is ConstantExpression constant)
                    {
                        var id = constant.Value;

                        var idEndpoint = GetIdEndpoint(id);
                        var endpoint = set.GetFullPath(idEndpoint, false).Result;

                        return new HttpRequestMessage(HttpMethod.Get, endpoint);
                    }

        throw new NotSupportedException("Unsupported LINQ expression");
    }

    private string GetIdEndpoint(object? id)
    {
        var setOptions = set.SetOptions;

        if (setOptions.GetIdEndpointAction is not null)
        {
            return setOptions.GetIdEndpointAction(id, null);
        }
        else
        {
            return setOptions.Endpoint is not null ? Path.Combine(setOptions.Endpoint, id!.ToString()!) : id!.ToString()!;
        }
    }
}
