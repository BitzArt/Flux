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
    /// The <see cref="IFluxJsonSetBuilder{TModel,TKey}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel, TKey> WithKey<TModel, TKey>(this IFluxJsonSetBuilder<TModel, TKey> builder, Expression<Func<TModel, TKey>> expression)
        where TModel : class
        where TKey : notnull
    {
        var options = builder.SetConfiguration.DataCollection;

        var keyPropertySelector = expression.Compile();
        options.KeyPropertySelector = keyPropertySelector;

        return builder;
    }
}