using System.Text.Json;

namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for configuring <see cref="JsonSerializerOptions"/>
/// </summary>
public static class ConfigureJsonSerializerExtension
{
    /// <summary>
    /// Configures JSON serialization for the <see cref="IFluxJsonServiceBuilder"/>
    /// </summary>
    /// <param name="builder">The <see cref="IFluxJsonServiceBuilder"/> to configure <see cref="JsonSerializerOptions"/> for.</param>
    /// <param name="configure"><see cref="JsonSerializerOptions"/> configuration action.</param>
    public static IFluxJsonServiceBuilder ConfigureJsonSerializer(this IFluxJsonServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure.Invoke(builder.ServiceConfiguration.JsonSerializerOptions);

        return builder;
    }
}
