using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for configuring a REST Flux service.
/// </summary>
public static class UsingJsonExtension
{
    /// <summary>
    /// Configures the <see cref="IFluxServiceProtocolConfigurator"/> by configuring it to use a Json service.
    /// </summary>
    /// <param name="protocolConfigurator">Flux service protocol configurator.</param>
    /// <param name="baseFilePath">Base file path to use when resolving JSON files.</param>
    /// <param name="configureJsonSerializer"><see cref="JsonSerializerOptions"/> configuration action.</param>
    /// <returns>
    /// A <see cref="IFluxJsonServiceBuilder"/> for further service configuration.
    /// </returns>
    [SuppressMessage(
        "Performance",
        "CA1859:Use concrete types when possible for improved performance",
        Justification = "Interface cast necessary to access default implementation methods.")]
    public static IFluxJsonServiceBuilder UsingJson(this IFluxServiceProtocolConfigurator protocolConfigurator, string? baseFilePath = null, Action<JsonSerializerOptions>? configureJsonSerializer = null)
    {
        IFluxJsonServiceBuilder builder = new FluxJsonServiceBuilder(protocolConfigurator.FluxBuilder, protocolConfigurator.ServiceName, baseFilePath);

        builder.ServiceCollection.AddLogging();

        configureJsonSerializer?.Invoke(builder.ServiceConfiguration.JsonSerializerOptions);

        protocolConfigurator.OnServiceProtocolConfigured(builder.ServiceName, builder);

        return builder;
    }
}
