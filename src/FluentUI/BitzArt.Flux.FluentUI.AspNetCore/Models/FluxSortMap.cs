using System.Linq.Expressions;
using System.Reflection;

namespace BitzArt.Flux;

public class FluxSortMap<TModel>
    where TModel : class
{
    private readonly Dictionary<MemberInfo, string> _map = [];

    public void Add<TValue>(Expression<Func<TModel, TValue>> memberExpression, string value)
    {
        var memberInfo = GetMember(memberExpression);
        _map.Add(memberInfo, value);
    }

    public string? GetSortValue(LambdaExpression expression)
    {
        var memberInfo = GetMember(expression);
        return _map.TryGetValue(memberInfo, out var value) ? value : null;
    }

    private MemberInfo GetMember(LambdaExpression expression)
    {
        var body = expression.Body;
        if (body is not MemberExpression memberExpression) throw new ArgumentException("Member expression expected", nameof(expression));

        return memberExpression.Member;
    }

    
}
