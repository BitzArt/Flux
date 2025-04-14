using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BitzArt.Flux.Sets;

/// <summary>
/// Extension methods for <see cref="IFluxServiceBuilder"/>.
/// </summary>
public static class AddSetContextExtension
{
    /// <summary>
    /// <para>
    /// Registers a set context with all possible signatures in the service collection,
    /// as well as an unspecified key wrapper for the provided set context, if necessary.
    /// </para>
    /// <para>
    /// This is an internal method and should not be used directly. <br />
    /// It is only exposed for the purposes of creating new <see href="https://bitzart.github.io/Flux/04.implementations.html">Flux Implementations</see>.
    /// </para>
    /// </summary>
    /// <typeparam name="TSetContext"></typeparam>
    /// <param name="builder"><see cref="IFluxServiceBuilder"/> instance to add the set context to.</param>"
    /// <param name="setName">Name of the set to add.</param>
    /// <param name="setLifetime">Lifetime of the set context.</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static IFluxServiceBuilder AddSetContext<TSetContext>(this IFluxServiceBuilder builder, string? setName, ServiceLifetime setLifetime)
        where TSetContext : class
    {
        var implementationType = typeof(TSetContext);
        var registrationInterface = implementationType
            .GetInterfaces()
            .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IFluxSetContext<,>))
            ?? throw new InvalidOperationException(
                $"The type '{implementationType.Name}' does not implement the required interface 'IFluxSetContext<TModel, TKey>'.");

        var possibleSignatures = GetPossibleSignatures(builder.ServiceName, setName).ToList();
        
        foreach (var signature in possibleSignatures)
        {
            var serviceDescriptor = new ServiceDescriptor(registrationInterface, signature, implementationType, setLifetime);
            builder.FluxBuilder.ServiceCollection.Add(serviceDescriptor);
        }

        var genericArguments = registrationInterface.GetGenericArguments();
        var modelType = genericArguments[0];
        var keyType = genericArguments[1];

        if (keyType != typeof(object))
        {
            Type[] wrapperRegistrationInterfaces =
            [
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
                    var serviceDescriptor = new ServiceDescriptor(wrapperRegistrationInterface, signature, (sp, _) =>
                    {
                        // retrieve actual set context
                        var actualSetContext = sp.GetRequiredKeyedService(implementationType, signature);

                        // create wrapper instance using the actual set context as a constructor argument
                        var wrapperInstance = Activator.CreateInstance(wrapperType, actualSetContext)!;

                        // return the wrapper instance
                        return wrapperInstance;
                    }, setLifetime);

                    builder.FluxBuilder.ServiceCollection.Add(serviceDescriptor);
                }
            }
        }

        return builder;
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
