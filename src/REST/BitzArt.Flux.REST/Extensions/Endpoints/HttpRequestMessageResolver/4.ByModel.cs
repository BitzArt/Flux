using BitzArt.Flux.REST;

namespace BitzArt.Flux;

public static partial class HttpRequestMessageResolverConfigurationExtensions
{
    // ==============================================================
    //                TModel => HttpRequestMessage
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, HttpRequestMessage> requestMessageResolver, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((TModel value, IServiceProvider _) => requestMessageResolver.Invoke(value), methods);

    // ==============================================================
    //       TModel, IServiceProvider => HttpRequestMessage
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, HttpRequestMessage> requestMessageResolver)
        where TModel : class
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,HttpRequestMessage},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, HttpRequestMessage> requestMessageResolver, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((ModelOperationDescriptor operation, IServiceProvider serviceProvider) => requestMessageResolver.Invoke((TModel)operation.Value!, serviceProvider), methods);


}
