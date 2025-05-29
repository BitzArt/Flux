using BitzArt.Flux.REST;
using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring <see cref="IFluxRestSetBuilder{TModel, TKey}"/>
/// </summary>
public static partial class PathEndpointConfigurationExtensions
{
    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, string path)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(path, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, string path)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(path, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, string path)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(path, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, string path)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(path, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey}(IFluxRestSetBuilder{TModel,TKey},string,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, string path)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(path, HttpMethods.Delete);

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
    /// <param name="methods">Allowed HTTP methods for this endpoint resolver.</param>
    /// <returns><see cref="IFluxRestSetBuilder{TModel, TKey}"/> to allow chaining.</returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, string path, HttpMethods methods = HttpMethods.All)
        where TModel : class
    {
        var setConfiguration = builder.SetConfiguration;

        var endpointConfiguration = new FluxRestPathEndpointConfiguration(setConfiguration, methods, path, allIncluded: false);

        setConfiguration.Add(endpointConfiguration);

        return builder;
    }
}
