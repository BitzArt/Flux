using BitzArt.Flux.REST;
using BitzArt.Pagination;

namespace BitzArt.Flux;

public static partial class PathResolverConfigurationExtensions
{
    // ==============================================================
    //                  PageRequest => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, string> resolvePath, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((PageRequest pageRequest, IServiceProvider _) => resolvePath.Invoke(pageRequest), queryComplete, methods);

    // ==============================================================
    //            PageRequest, IServiceProvider => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, IServiceProvider, string> resolvePath, bool queryComplete = false)
        where TModel : class
        => builder.WithEndpoint(resolvePath, queryComplete, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},bool,HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<PageRequest, IServiceProvider, string> resolvePath, bool queryComplete = false, HttpMethods methods = HttpMethods.All)
        where TModel : class
        => builder.WithEndpoint((GetPageOperationDescriptor descriptor, IServiceProvider serviceProvider) => resolvePath.Invoke(descriptor.PageRequest, serviceProvider), queryComplete, methods);
}
