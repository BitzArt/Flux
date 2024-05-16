using System.Linq.Expressions;
using System.Reflection;

namespace BitzArt.Flux;

/// <summary>
/// Represents a sorting map for a Flux request.
/// </summary>
/// <typeparam name="TModel"></typeparam>
public class FluxSortMap<TModel>
    where TModel : class
{
    private readonly Dictionary<MemberInfo, string> _map = [];

    /// <summary>
    /// Adds a sort value to the map.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="memberExpression"></param>
    /// <param name="value"></param>
    public void Add<TValue>(Expression<Func<TModel, TValue>> memberExpression, string value)
    {
        var memberInfo = GetMember(memberExpression);
        _map.Add(memberInfo, value);
    }

    /// <summary>
    /// Gets the sort value for the specified expression.
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
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
