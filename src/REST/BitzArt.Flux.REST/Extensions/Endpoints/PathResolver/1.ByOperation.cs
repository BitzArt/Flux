using BitzArt.Flux.REST;
using BitzArt.Flux.REST.Endpoints;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static partial class PathResolverConfigurationExtensions
{
    // ==============================================================
    //     TOperationDescriptor => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Delete);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, string> resolvePath, HttpMethods methods = HttpMethods.All)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint((TOperationDescriptor descriptor, IServiceProvider _) => resolvePath.Invoke(descriptor), methods);

    // ==============================================================
    //     TOperationDescriptor, IServiceProvider => string
    // ==============================================================

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithGet<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Get);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPost<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Post);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPut<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Put);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithPatch<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, string> requestMessageResolver)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
        => builder.WithEndpoint(requestMessageResolver, HttpMethods.Patch);

    /// <inheritdoc cref="WithEndpoint{TModel,TKey,TOperationDescriptor}(IFluxRestSetBuilder{TModel,TKey},Func{TOperationDescriptor,IServiceProvider,string},HttpMethods)"/>
    public static IFluxRestSetBuilder<TModel, TKey> WithDelete<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, string> requestMessageResolver)
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
    /// <param name="resolvePath">A function that resolves the endpoint path for the given operation.</param>
    /// <param name="methods">Allowed HTTP methods for this endpoint resolver.</param>
    /// <returns><see cref="IFluxRestSetBuilder{TModel, TKey}"/> to allow chaining.</returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, string> resolvePath, HttpMethods methods = HttpMethods.All)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
    {
        var setConfiguration = builder.SetConfiguration;

        var endpointConfiguration = new FluxRestResolverEndpointConfiguration<TOperationDescriptor>(setConfiguration, methods, (descriptor, serviceProvider) =>
        {
            var requestMessageResolver = serviceProvider.GetRequiredService<IHttpRequestMessageResolver>();
            
            var path = resolvePath.Invoke(descriptor, serviceProvider);
            
            var requestMessage = requestMessageResolver.Resolve(setConfiguration, path, descriptor, true);

            return requestMessage;
        });
        setConfiguration.Add(endpointConfiguration);

        return builder;
    }
}
