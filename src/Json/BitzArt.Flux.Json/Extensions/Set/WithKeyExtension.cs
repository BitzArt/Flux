using System.Linq.Expressions;

namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for configuring the key property of a set.
/// </summary>
public static class WithKeyExtension
{
    /// <summary>
    /// Configures the key property for the set.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type of the set.
    /// </typeparam>
    /// <typeparam name="TKey">
    /// The key type of the set.
    /// </typeparam>
    /// <param name="builder"></param>
    /// <param name="expression">
    /// The expression to select the key property.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel> WithKey<TModel, TKey>(this IFluxJsonSetBuilder<TModel> builder, Expression<Func<TModel, TKey>> expression)
        where TModel : class
    {
        var options = builder.SetConfiguration.DataCollection;

        options.KeyPropertySelector = expression.CastToObject().Compile();

        return builder;
    }

    public static Expression<Func<TModel, object>> CastToObject<TModel, TKey>(this Expression<Func<TModel, TKey>> expr)
    {
        var parameter = expr.Parameters[0];

        Expression body = expr.Body;

        // Если значение-тип, делаем boxing через Convert
        if (body.Type.IsValueType)
        {
            body = Expression.Convert(body, typeof(object));
        }

        return Expression.Lambda<Func<TModel, object>>(body, parameter);
    }
}