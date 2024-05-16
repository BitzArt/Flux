using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BitzArt.Flux;

/// <summary>
/// Extensions for adding ItemsProviders.
/// </summary>
public static class AddItemsProviderExtensions
{
    /// <summary>
    /// Adds ItemsProviders from all loaded assemblies to the service collection.
    /// </summary>
    public static IServiceCollection AddItemsProviders(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies) services.AddItemsProvidersFromAssembly(assembly);
        return services;
    }

    /// <summary>
    /// Adds ItemsProviders from the assembly containing the specified type to the service collection.
    /// </summary>
    public static IServiceCollection AddItemsProvidersFromAssemblyContaining<T>(this IServiceCollection services)
        => services.AddItemsProvidersFromAssembly(typeof(T).Assembly);

    /// <summary>
    /// Adds ItemsProviders from the specified assembly to the service collection.
    /// </summary>
    public static IServiceCollection AddItemsProvidersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var providerTypes = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IFluxItemsProvider).IsAssignableFrom(t));

        return services.AddItemsProviders(providerTypes);
    }

    /// <summary>
    /// Adds ItemsProviders to the service collection.
    /// </summary>
    public static IServiceCollection AddItemsProviders(this IServiceCollection services, params Type[] providerTypes)
        => services.AddItemsProviders(providerTypes);

    /// <summary>
    /// Adds ItemsProviders to the service collection.
    /// </summary>
    public static IServiceCollection AddItemsProviders(this IServiceCollection services, IEnumerable<Type> providerTypes)
    {
        foreach (var providerType in providerTypes) services.AddItemsProvider(providerType);
        return services;
    }

    /// <summary>
    /// Adds an ItemsProvider to the service collection.
    /// </summary>
    public static IServiceCollection AddItemsProvider<TProvider>(this IServiceCollection services)
        where TProvider : IFluxItemsProvider
        => services.AddItemsProvider(typeof(TProvider));

    /// <summary>
    /// Adds an ItemsProvider to the service collection.
    /// </summary>
    public static IServiceCollection AddItemsProvider(this IServiceCollection services, Type providerType)
    {
        services.AddTransient(providerType);
        services.AddTransient(typeof(IFluxItemsProvider), x => x.GetRequiredService(providerType));

        var genericInterface = providerType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType
                && i.GetGenericTypeDefinition() == typeof(IFluxItemsProvider<>));

        if (genericInterface is not null)
            services.AddTransient(genericInterface, x => x.GetRequiredService(providerType));

        return services;
    }
}
