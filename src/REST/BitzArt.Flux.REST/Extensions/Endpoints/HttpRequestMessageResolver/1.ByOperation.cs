using BitzArt.Flux.REST;
using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux;

public static partial class HttpRequestMessageResolverConfigurationExtensions
{
    // ==============================================================
    //       TOperationDescriptor => HttpRequestMessage
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, HttpRequestMessage> requestMessageResolver, HttpMethods methods = HttpMethods.All)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint((TOperationDescriptor operation, IServiceProvider _) => requestMessageResolver.Invoke(operation), methods);

    // ==============================================================
    //  TOperationDescriptor, IServiceProvider => HttpRequestMessage
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Delete);

    /// <summary>
    /// Configures the <see cref="IFluxRestSetBuilder{TModel, TKey}"/> to use a custom endpoint resolver for the given operation type.
    /// </summary>
    /// <typeparam name="TModel">Set model type.</typeparam>
    /// <typeparam name="TKey">Set key type.</typeparam>
    /// <typeparam name="TOperationDescriptor"></typeparam>
    /// <param name="builder">Flux REST set builder.</param>
    /// <param name="requestMessageResolver">A function that resolves the <see cref="HttpRequestMessage"/> for the given operation.</param>
    /// <param name="methods">Allowed HTTP methods for this endpoint resolver.</param>
    /// <returns><see cref="IFluxRestSetBuilder{TModel, TKey}"/> to allow chaining.</returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, HttpRequestMessage> requestMessageResolver, HttpMethods methods = HttpMethods.All)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
    {
        var setConfiguration = builder.SetConfiguration;
        var endpointConfiguration = new FluxRestResolverEndpointConfiguration<TOperationDescriptor>(setConfiguration, methods, requestMessageResolver);
        setConfiguration.Add(endpointConfiguration);

        return builder;
    }
}
