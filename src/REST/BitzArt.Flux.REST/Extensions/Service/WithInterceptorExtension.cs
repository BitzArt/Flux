using BitzArt.Flux.REST;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring REST Flux service interceptors.
/// </summary>
public static class WithInterceptorExtension
{
    public static IFluxRestServiceBuilder WithInterceptor<TInterceptor>(this IFluxRestServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where TInterceptor : class, IFluxRestInterceptor
    {
        var serviceDescriptor = new ServiceDescriptor(typeof(IFluxRestInterceptor), builder.ServiceName, typeof(TInterceptor), serviceLifetime);


        return builder;
    }
}
