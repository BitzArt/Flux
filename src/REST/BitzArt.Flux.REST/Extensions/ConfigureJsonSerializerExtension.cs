using BitzArt.Flux.REST;
using System.Text.Json;

namespace BitzArt.Flux;

/// <summary>
/// Extension methods for configuring <see cref="JsonSerializerOptions"/>
/// </summary>
public static class ConfigureJsonSerializerExtension
{
    /// <summary>
    /// Configures JSON serialization for the <see cref="IFluxRestServiceBuilder"/>
    /// </summary>
    /// <param name="builder">The <see cref="IFluxRestServiceBuilder"/> to configure <see cref="JsonSerializerOptions"/> for.</param>
    /// <param name="configure"><see cref="JsonSerializerOptions"/> configuration action.</param>
    public static IFluxRestServiceBuilder ConfigureJsonSerializer(this IFluxRestServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure.Invoke(builder.ServiceConfiguration.SerializerOptions);

        return builder;
    }
}
