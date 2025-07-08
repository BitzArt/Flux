using BitzArt.Flux.Rest.Endpoints;

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
    /// <para>
    /// Configures the <see cref="IFluxRestSetBuilder{TModel, TKey}"/> to use a custom endpoint for the given <see cref="HttpMethods"/>.
    /// </para>
    /// <para>
    /// <b>Note:</b> provide a base set path here (e.g. <c>"books"</c>). <br />
    /// Endpoints using Id or PageRequest will automatically append the Id or PageRequest to this path
    /// (e.g. <c>"books/1"</c> or <c>"books?limit=10"</c>).
    /// </para>
    /// </summary>
    /// <typeparam name="TModel">Set model type.</typeparam>
    /// <typeparam name="TKey">Set key type.</typeparam>
    /// <param name="builder">Flux REST set builder.</param>
    /// <param name="path">Endpoint path.</param>
    /// <param name="pathComplete">
    /// Indicates whether the resolved path is complete, meaning it does not require additional parts to be appended
    /// (e.g., Id part for operations that use Ids).
    /// </param>
    /// <param name="queryComplete">
    /// <para>
    /// Indicates whether the resolved path's HTTP query string is complete,
    /// meaning it does not require additional query parameters to automatically be appended
    /// (e.g., <see cref="PageRequest"/> parameters for operations that use pagination).
    /// </para>
    /// <para>
    /// If <see langword="true"/>, unused named operation parameters will not automatically be appended to the query string.
    /// </para>
    /// <param name="methods">Allowed HTTP methods for this endpoint resolver.</param>
    /// <returns><see cref="IFluxRestSetBuilder{TModel, TKey}"/> to allow chaining.</returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string path, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
    {
        var setConfiguration = builder.SetConfiguration;

        var endpointConfiguration = new FluxRestPathEndpointConfiguration(setConfiguration, methods, path, false, queryComplete);

        setConfiguration.Add(endpointConfiguration);

        return builder;
    }
}
