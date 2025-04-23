using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BitzArt.Flux.Builder;

internal class FluxBuilder : IFluxBuilder
{
    public IServiceCollection ServiceCollection { get; private init; }

    public FluxBuilder(IServiceCollection serviceCollection)
    {
        ServiceCollection = serviceCollection;
    }

    public IFluxServiceBuilder AddService(string serviceName)
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

        ServiceCollection.Add(descriptors);

        return new FluxServiceBuilder(this, serviceName);
    }
}
