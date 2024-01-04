using System.Linq.Expressions;

namespace BitzArt.Flux;

public static class WithKeyExtension
{
    public static IFluxJsonSetBuilder<TModel> WithKey<TModel, TKey>(this IFluxJsonSetBuilder<TModel> builder,
        Expression<Func<TModel, TKey>> expression)
        where TModel : class
    {
        return builder;
    }
}