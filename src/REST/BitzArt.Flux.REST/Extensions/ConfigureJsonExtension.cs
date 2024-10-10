using System.Text.Json;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring JSON serialization in <see cref="IFluxRestServiceBuilder"/>
/// </summary>
public static class ConfigureJsonExtension
{
    /// <summary>
    /// Configures JSON serialization for the <see cref="IFluxRestServiceBuilder"/>
    /// </summary>
    public static IFluxRestServiceBuilder ConfigureJson(this IFluxRestServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure(builder.ServiceOptions.SerializerOptions);

        return builder;
    }
}
