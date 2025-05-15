using BitzArt.Flux.REST;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring <see cref="IFluxRestSetBuilder{TModel, TKey}"/>
/// </summary>
public static class FluxRestSetBuilderExtensions
{
    /// <summary>
    /// Configures the <see cref="IFluxRestSetBuilder{TModel, TKey}"/> to use a custom endpoint.
    /// </summary>
    /// <typeparam name="TModel">Set model type.</typeparam>
    /// <typeparam name="TKey">Set key type.</typeparam>
    /// <typeparam name="TOperationDescriptor"></typeparam>
    /// <param name="builder"></param>
    /// <param name="requestMessageFactory"></param>
    /// <param name="methods"></param>
    /// <returns></returns>
    public static IFluxRestSetBuilder<TModel, TKey> WithEndpoint<TModel, TKey, TOperationDescriptor>(this IFluxRestSetBuilder<TModel, TKey> builder, Func<TOperationDescriptor, IServiceProvider, HttpRequestMessage> requestMessageFactory, HttpMethods methods = HttpMethods.All)
        where TModel : class
        where TOperationDescriptor : OperationDescriptor
    {
        throw new NotImplementedException();
    }
}
