using BitzArt.Flux.Builder;
using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for adding a service to an <see cref="IFluxBuilder"/>.
/// </summary>
public static class AddServiceExtension
{
    /// <summary>
    /// Adds a service to an <see cref="IFluxBuilder"/>. <br/>
    /// See <see href="https://bitzart.github.io/Flux/02.configure.html">Configure Flux</see> for more information.
    /// </summary>
    /// <param name="fluxBuilder"><see cref="IFluxBuilder"/> instance to add the service to.</param>
    /// <param name="serviceName">Name of the service to add.</param>
    public static IFluxServiceBuilder AddService(this IFluxBuilder fluxBuilder, string serviceName)
    {
        ServiceDescriptor[] descriptors =
        [
            ServiceDescriptor.KeyedScoped(
                typeof(IFluxServiceContext),
                new FluxServiceSignature(serviceName),
                (sp, _) => new FluxServiceContext(sp, serviceName)),

            ServiceDescriptor.Scoped(
                typeof(IFluxServiceContext),
                sp => new FluxServiceContext(sp, serviceName)),
        ];

        fluxBuilder.ServiceCollection.Add(descriptors);

        return new FluxServiceBuilder(fluxBuilder, serviceName);
    }
}
