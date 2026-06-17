namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for enriching the query building logic of a set.
/// </summary>
public static class EnrichQueryExtension
{
    /// <summary>
    /// Configures the query building logic of a set
    /// </summary>
    /// <typeparam name="TModel">The model type of the set</typeparam>
    /// <param name="builder"></param>
    /// <param name="enrichQuery"></param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel> EnrichQuery<TModel>(this IFluxJsonSetBuilder<TModel> builder,
        Func<IQueryable<TModel>, OperationDescriptor, IQueryable<TModel>> enrichQuery)
        where TModel : class
    {
        builder.SetConfiguration.EnrichQuery = enrichQuery;
        return builder;
    }
}
