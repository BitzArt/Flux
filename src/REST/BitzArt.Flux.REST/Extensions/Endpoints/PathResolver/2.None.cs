namespace BitzArt.Flux.Rest;

public static partial class PathResolverConfigurationExtensions
{
    // ==============================================================
    //                          => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<string> resolvePath, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((IServiceProvider _) => resolvePath.Invoke(), queryComplete, methods);

    // ==============================================================
    //                 IServiceProvider => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<IServiceProvider, string> resolvePath, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((OperationDescriptor _, IServiceProvider serviceProvider) => resolvePath.Invoke(serviceProvider), queryComplete, methods);
}
