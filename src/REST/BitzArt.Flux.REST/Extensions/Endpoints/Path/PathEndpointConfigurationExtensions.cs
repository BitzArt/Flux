using BitzArt.Flux.Rest.Endpoints;
using BitzArt.Pagination;

namespace BitzArt.Flux.Rest;

/// <summary>
/// Extension methods for configuring <see cref="IFluxRestSetBuilder{TModel, TKey}"/>
/// </summary>
public static partial class PathEndpointConfigurationExtensions
{
    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string path, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(path, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string path, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(path, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string path, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(path, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string path, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(path, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string path, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(path, queryComplete, HttpMethods.Delete);

    /// <summary>
    /// Configures an endpoint path for operations that use any of the specified HTTP methods.
    /// </summary>
    /// <typeparam name="TModel">Set model type.</typeparam>
    /// <typeparam name="TKey">Set key type.</typeparam>
    /// <param name="builder">Flux REST set builder.</param>
    /// <param name="path">
    /// Endpoint path appended after the Service and Set paths. When an operation targets a model by identifier,
    /// Flux.REST appends that identifier to this path.
    /// </param>
    /// <param name="queryComplete">
    /// <see langword="true"/> to preserve the query in <paramref name="path"/> without appending unused named parameters
    /// or <see cref="PageRequest"/> parameters; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="methods">HTTP methods that use this endpoint path.</param>
    /// <returns>The supplied builder for method chaining.</returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string path, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
    {
        var setConfiguration = builder.SetConfiguration;

        var endpointConfiguration = new FluxRestPathEndpointConfiguration(setConfiguration, methods, path, false, queryComplete);

        setConfiguration.Add(endpointConfiguration);

        return builder;
    }
}
