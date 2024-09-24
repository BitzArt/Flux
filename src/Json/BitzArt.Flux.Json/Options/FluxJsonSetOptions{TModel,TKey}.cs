using System.Linq.Expressions;

namespace BitzArt.Flux;

public class FluxJsonSetOptions<TModel, TKey> : FluxJsonSetOptions<TModel>
    where TModel : class
{
    public new Expression<Func<TModel, TKey>>? KeyPropertyExpression
    {
        get
        {
            if (_keyPropertyExpression is null) return null;

            Expression<Func<TModel, TKey>> result;
            
            if (typeof(TKey).IsValueType)
                result = Expression.Lambda<Func<TModel, TKey>>(Expression.Convert(_keyPropertyExpression.Body, typeof(TKey)), _keyPropertyExpression.Parameters);
            else
                result = Expression.Lambda<Func<TModel, TKey>>(_keyPropertyExpression.Body, _keyPropertyExpression.Parameters);

            return result;
        }
        set
        {
            if (value is null) return;

            Expression<Func<TModel, object>> result;
            
            if (typeof(TKey).IsValueType)
                result = Expression.Lambda<Func<TModel, object>>(Expression.Convert(value.Body, typeof(object)), value.Parameters);
            else
                result = Expression.Lambda<Func<TModel, object>>(value.Body, value.Parameters);

            _keyPropertyExpression = result;
        }
    }
}