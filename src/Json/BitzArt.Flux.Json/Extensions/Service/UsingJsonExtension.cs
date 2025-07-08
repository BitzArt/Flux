using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for configuring a JSON Flux service.
/// </summary>
public static class UsingJsonExtension
{
    // AddService("my-service")
    // .UsingJson()
    // .ConfigureJsonSerializer(x =>
    // {
    //
    // });

    // AddService("my-service")
    // .UsingJson("", x =>
    // {
    //
    // });

    /// <summary>
    /// Configures the service to use a JSON Flux implementation.
    /// </summary>
    /// <param name="protocolConfigurator">Flux service protocol configurator.</param>
    /// <param name="baseFilePath">Base file path to use when resolving JSON files.</param>
    /// <returns>
    /// A <see cref="IFluxJsonServiceBuilder"/> for further JSON service configuration.
    /// </returns>
    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible for improved performance",
        Justification = "Interface cast necessary to access default implementation methods.")]
    public static IFluxJsonServiceBuilder UsingJson(this IFluxServiceProtocolConfigurator protocolConfigurator, string? baseFilePath = null)
    {
        IFluxJsonServiceBuilder builder = new FluxJsonServiceBuilder(protocolConfigurator.FluxBuilder, protocolConfigurator.ServiceName, baseFilePath);

        builder.ServiceCollection.AddLogging();

        protocolConfigurator.OnServiceProtocolConfigured(builder.ServiceName, builder);

        return builder;
    }
}
