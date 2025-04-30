using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BitzArt.Flux.Sets;

/// <summary>
/// Extension methods for <see cref="IFluxServiceBuilder"/>.
/// </summary>
public static class AddFluxSetContextExtension
{
    /// <summary>
    /// <para>
    /// Registers a set context in the service collection using all possible signature combinations; <br />
    /// Registers an unspecified key wrapper for the provided set context, if necessary.
    /// </para>
    /// <para>
    /// <b>Note:</b> This method is a part of internal implementation details and should not be used directly. <br />
    /// It is only exposed for the purposes of <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementations</see>.
    /// </para>
    /// </summary>
    /// <param name="services">Service collection to add the set context to.</param>
    /// <param name="serviceName">Name of the service this set context belongs to.</param>
    /// <param name="implementationFactory">Factory method to create the set context.</param>
    /// <param name="setName">Name of the set to add.</param>
    /// <param name="setLifetime">Lifetime of the set context.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IServiceCollection AddSetContext<TModel, TKey>(this IServiceCollection services, string serviceName, Func<IServiceProvider, IFluxSetContext<TModel, TKey>> implementationFactory, string? setName, ServiceLifetime setLifetime)
        where TModel : class
        where TKey : notnull
    {
        var registrationInterface = typeof(IFluxSetContext<TModel, TKey>);

        var possibleSignatures = GetPossibleSignatures(serviceName, setName).ToList();

        foreach (var signature in possibleSignatures)
        {
            ServiceDescriptor[] serviceDescriptors =
            [
                // keyed descriptor for package internal consumption
                new(registrationInterface, signature, (sp, key) => implementationFactory.Invoke(sp), setLifetime),

                // unkeyed for arbitrary injection
                new(registrationInterface, implementationFactory, setLifetime)
            ];
            services.Add(serviceDescriptors);
        }

        var genericArguments = registrationInterface.GetGenericArguments();
        var modelType = genericArguments[0];
        var keyType = genericArguments[1];

        Type[] wrapperRegistrationInterfaces = keyType == typeof(object)
            // if key is already of type 'object',
            // register a wrapper to implement IFluxSetContext<TModel> (no TKey argument)
            ? [
                // IFluxSetContext<TModel>
                typeof(IFluxSetContext<>).MakeGenericType(modelType)
            ]
            // if key is not already of type 'object',
            // register a wrapper for both implicit and explicit TKey variants
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

                services.Add(serviceDescriptors);
            }
        }

        return services;
    }

    private static IEnumerable<FluxSetSignature> GetPossibleSignatures(string serviceName, string? setName)
    {
        // register the set context for queries with
        // both specified and unspecified service name parameter
        string?[] possibleServiceNames = [serviceName, null];

        // if the set is registered using a name,
        // register the set context for queries with
        // both named and unnamed variants of the set context
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
