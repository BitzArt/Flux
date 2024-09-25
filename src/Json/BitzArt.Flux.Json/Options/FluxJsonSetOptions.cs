using System.Linq.Expressions;

namespace BitzArt.Flux;

internal class FluxJsonSetOptions<TModel, TKey> : IFluxJsonSetOptions<TModel>
    where TModel : class
{
    public ICollection<TModel>? Items { get; set; }

    public Expression<Func<TModel, TKey>>? KeyPropertyExpression;

    Expression<Func<TModel, object>>? IFluxJsonSetOptions<TModel>.KeyPropertyExpression
    {
        get
        {
            if (KeyPropertyExpression is null) return null;

            var convert = Expression.Convert(KeyPropertyExpression.Body, typeof(object));
            return Expression.Lambda<Func<TModel, object>>(convert, KeyPropertyExpression.Parameters);
        }

        set
        {
            if (value is null)
            {
                KeyPropertyExpression = null;
            }
            else
            {
                var convert = Expression.Convert(value.Body, typeof(TKey));
                KeyPropertyExpression = Expression.Lambda<Func<TModel, TKey>>(convert, value.Parameters);
            }
        }
    }
}