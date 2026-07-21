namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for customizing read queries for JSON-backed sets.
/// </summary>
public static class EnrichQueryExtension
{
    /// <summary>
    /// Configures a transformation that is applied to the set query before a read operation is resolved.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The transformation applies to <see cref="GetOperationDescriptor"/>,
    /// <see cref="GetAllOperationDescriptor"/>, and <see cref="GetPageOperationDescriptor"/> operations.
    /// </para>
    /// </remarks>
    /// <typeparam name="TModel">Model type stored in the set.</typeparam>
    /// <param name="builder">Set builder to configure.</param>
    /// <param name="enrichQuery">
    /// Transformation that receives the current query and the descriptor for the read operation.
    /// </param>
    /// <returns><see cref="IFluxJsonSetBuilder{TModel}"/> for further configuration.</returns>
    public static IFluxJsonSetBuilder<TModel> EnrichQuery<TModel>(this IFluxJsonSetBuilder<TModel> builder,
        Func<IQueryable<TModel>, OperationDescriptor, IQueryable<TModel>> enrichQuery)
        where TModel : class
    {
        ArgumentNullException.ThrowIfNull(enrichQuery, nameof(enrichQuery));
        
        if (builder.SetConfiguration.EnrichQuery is not null)
        {
            throw new InvalidOperationException($"The set configuration for type {typeof(TModel).Name} already has an enrich query configured.");
        }
        
        builder.SetConfiguration.EnrichQuery = enrichQuery;
        return builder;
    }
}
