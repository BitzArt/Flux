namespace BitzArt.Flux.Json;

public static class BuildQueryExtension
{
    public static IFluxJsonSetBuilder<TModel> BuildQuery<TModel>(this IFluxJsonSetBuilder<TModel> builder,
        Func<IQueryable<TModel>, OperationDescriptor, IQueryable<TModel>> buildQuery)
        where TModel : class
    {
        builder.SetConfiguration.BuildQuery = buildQuery;
        return builder;
    }
}
