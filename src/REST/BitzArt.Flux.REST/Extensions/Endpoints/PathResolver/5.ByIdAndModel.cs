using BitzArt.Flux.REST;

namespace BitzArt.Flux;

public static partial class PathResolverConfigurationExtensions
{
    // ==============================================================
    //                   TKey, TModel => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, string> resolvePath, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((TKey id, TModel value, IServiceProvider _) => resolvePath.Invoke(id, value), queryComplete, methods);

    // ==============================================================
    //          TKey, TModel, IServiceProvider => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TKey, TModel, IServiceProvider, string> resolvePath, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((ModelOperationDescriptor descriptor, IServiceProvider serviceProvider) => resolvePath.Invoke((TKey)descriptor.Id!, (TModel)descriptor.Value!, serviceProvider), queryComplete, methods);
}
