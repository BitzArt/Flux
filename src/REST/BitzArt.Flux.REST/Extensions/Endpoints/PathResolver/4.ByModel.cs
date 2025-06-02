using BitzArt.Flux.REST;

namespace BitzArt.Flux;

public static partial class PathResolverConfigurationExtensions
{
    // ==============================================================
    //                     TModel => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, string> resolvePath, bool pathComplete = false, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((TModel value, IServiceProvider _) => resolvePath.Invoke(value), pathComplete, queryComplete, methods);

    // ==============================================================
    //             TModel, IServiceProvider => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, string> resolvePath, bool pathComplete = false, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, pathComplete, queryComplete, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TModel, IServiceProvider, string> resolvePath, bool pathComplete = false, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((ModelOperationDescriptor descriptor, IServiceProvider serviceProvider) => resolvePath.Invoke((TModel)descriptor.Value!, serviceProvider), pathComplete, queryComplete, methods);
}
