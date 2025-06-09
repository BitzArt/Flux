using BitzArt.Flux.REST;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring REST Flux service interceptors.
/// </summary>
public static class WithInterceptorExtension
{
    /// <summary>
    /// Configures an <see cref="IFluxRestInterceptor"/> for a Flux REST service.
    /// </summary>
    /// <remarks>
    /// When registered with <see cref="ServiceLifetime.Transient"/>,
    /// a single instance of the interceptor will be created for each request,
    /// handling both the request and response phases.
    /// </remarks>
    /// <typeparam name="TInterceptor">Interceptor type that implements <see cref="IFluxRestInterceptor"/>.</typeparam>
    /// <param name="builder">Flux REST service builder.</param>
    /// <param name="serviceLifetime">Interceptor service lifetime.</param>
    /// <returns></returns>
    public static IFluxRestServiceBuilder WithInterceptor<TInterceptor>(this IFluxRestServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TInterceptor : class, IFluxRestInterceptor
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(IFluxRestInterceptor), builder.ServiceName, typeof(TInterceptor), serviceLifetime);
        builder.ServiceCollection.Add(serviceDescriptor);

        return builder;
    }
}
