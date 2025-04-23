using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.Builder;

internal class FluxServiceBuilder : IFluxServiceBuilder
{
    public IFluxBuilder FluxBuilder { get; private init; }
    public string ServiceName { get; private init; }

    public FluxServiceBuilder(IFluxBuilder fluxBuilder, string name)
    {
        FluxBuilder = fluxBuilder;
        ServiceName = name;
    }

    public IFluxServiceBuilder AddSetContext<TModel, TKey>(Func<IServiceProvider, IFluxSetContext<TModel, TKey>> implementationFactory, string? setName, ServiceLifetime setLifetime)
        where TModel : class
        where TKey : notnull
    {
        var registrationInterface = typeof(IFluxSetContext<TModel, TKey>);

        var possibleSignatures = GetPossibleSignatures(ServiceName, setName).ToList();

        foreach (var signature in possibleSignatures)
        {
            ServiceDescriptor[] serviceDescriptors =
            [
                // keyed descriptor for internal consumption
                new(registrationInterface, signature, (sp, key) => implementationFactory.Invoke(sp), setLifetime),

                // unkeyed for arbitrary external injection
                new(registrationInterface, implementationFactory, setLifetime)
            ];
            FluxBuilder.ServiceCollection.Add(serviceDescriptors);
        }

        var genericArguments = registrationInterface.GetGenericArguments();
        var modelType = genericArguments[0];
        var keyType = genericArguments[1];

        Type[] wrapperRegistrationInterfaces = keyType == typeof(object)
            // if key is already of type 'object', register a wrapper to implement IFluxSetContext<TModel>
            ? [
                // IFluxSetContext<TModel>
                typeof(IFluxSetContext<>).MakeGenericType(modelType)
            ]
            // if key is not already of type 'object', register a wrapper for both implicit and explicit TKey variants
            : [
                // IFluxSetContext<TModel>
                typeof(IFluxSetContext<>).MakeGenericType(modelType),
                // IFluxSetContext<TModel, object>
                typeof(IFluxSetContext<,>).MakeGenericType(modelType, typeof(object))
            ];
        var wrapperType = typeof(FluxSetContextUnspecifiedKeyTypeWrapper<,>).MakeGenericType(modelType, keyType);

        foreach (var wrapperRegistrationInterface in wrapperRegistrationInterfaces)
        {
            foreach (var signature in possibleSignatures)
            {
                ServiceDescriptor[] serviceDescriptors =
                [
                    // keyed wrapper descriptor for internal consumption
                    new(wrapperRegistrationInterface, signature, (sp, _) =>
                        {
                            // retrieve actual set context
                            var actualSetContext = sp.GetRequiredKeyedService(registrationInterface, signature);

                            // create wrapper instance using the actual set context as a constructor argument
                            var wrapperInstance = Activator.CreateInstance(wrapperType, actualSetContext)!;

                            // return the wrapper instance
                            return wrapperInstance;
                        }, setLifetime),

                        // unkeyed wrapper descriptor for arbitrary external injection
                        new(wrapperRegistrationInterface, sp => sp.GetRequiredKeyedService(wrapperRegistrationInterface, signature), setLifetime)
                ];

                FluxBuilder.ServiceCollection.Add(serviceDescriptors);
            }
        }

        return this;
    }

    private static IEnumerable<FluxSetSignature> GetPossibleSignatures(string serviceName, string? setName)
    {
        // register the set context for both the specified and unspecified service name
        string?[] possibleServiceNames = [serviceName, null];

        // if the set is registered using a name, register both named and unnamed variants of the context
        string?[] possibleSetNames = setName is not null ? [setName, null] : [null];

        // enumerate all possible combinations of service and set names
        foreach (var possibleServiceName in possibleServiceNames)
        {
            var serviceSignature = new FluxServiceSignature(possibleServiceName);

            foreach (var possibleSetName in possibleSetNames)
            {
                yield return new FluxSetSignature(serviceSignature, possibleSetName);
            }
        }
    }
}
