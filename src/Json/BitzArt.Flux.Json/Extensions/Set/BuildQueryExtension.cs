namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for configuring the query building logic of a set.
/// </summary>
public static class BuildQueryExtension
{
    /// <summary>
    /// Configures the query building logic of a set
    /// </summary>
    /// <typeparam name="TModel">The model type of the set</typeparam>
    /// <param name="builder"></param>
    /// <param name="buildQuery"></param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel> BuildQuery<TModel>(this IFluxJsonSetBuilder<TModel> builder,
        Func<IQueryable<TModel>, OperationDescriptor, IQueryable<TModel>> buildQuery)
        where TModel : class
    {
        builder.SetConfiguration.BuildQuery = buildQuery;
        return builder;
    }
}
