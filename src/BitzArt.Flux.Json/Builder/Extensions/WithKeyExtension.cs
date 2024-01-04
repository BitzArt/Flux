using System.Linq.Expressions;

namespace BitzArt.Flux;

public static class WithKeyExtension
{
    public static IFluxJsonSetBuilder<TModel> WithKey<TModel>(this IFluxJsonSetBuilder<TModel> builder, Expression<Func<TModel, object>> expression)
        where TModel : class
    {
        builder.SetOptions.KeyPropertyExpression = expression;
        return builder;
    }

    public static IFluxJsonSetBuilder<TModel, TKey> WithKey<TModel, TKey>(this IFluxJsonSetBuilder<TModel, TKey> builder, Expression<Func<TModel, TKey>> expression)
        where TModel : class
    {
        builder.SetOptions.KeyPropertyExpression = expression;
        return builder;
    }
}