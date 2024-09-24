using System.Text.Json;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring JSON serializer in the service.
/// </summary>
public static class ConfigureJsonExtension
{
    /// <summary>
    /// Configures JSON serializer for the service.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configure">
    /// The configuration action to apply to the <see cref="JsonSerializerOptions"/>.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonServiceBuilder"/> for further service configuration.
    /// </returns>
    public static IFluxJsonServiceBuilder ConfigureJson(this IFluxJsonServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure(builder.ServiceOptions.SerializerOptions);

        return builder;
    }
}
