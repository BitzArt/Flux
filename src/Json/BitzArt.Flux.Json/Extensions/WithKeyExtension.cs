using System.Linq.Expressions;

namespace BitzArt.Flux;

public static class WithKeyExtension
{
    /// <summary>
    /// Configures the key property for the set.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type of the set.
    /// </typeparam>
    /// <param name="builder"></param>
    /// <param name="expression">
    /// The expression to select the key property.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel> WithKey<TModel>(this IFluxJsonSetBuilder<TModel> builder, Expression<Func<TModel, object>> expression)
        where TModel : class
    {
        builder.SetOptions.KeyPropertyExpression = expression;
        
        return builder;
    }

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
    /// The <see cref="IFluxJsonSetBuilder{TModel,TKey}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel, TKey> WithKey<TModel, TKey>(this IFluxJsonSetBuilder<TModel, TKey> builder, Expression<Func<TModel, TKey>> expression)
        where TModel : class
    {
        builder.SetOptions.KeyPropertyExpression = expression;
        
        return builder;
    }
}