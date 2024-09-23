using System.Text.Json;

namespace BitzArt.Flux;

public static class ConfigureJsonExtension
{
    public static IFluxRestServiceBuilder ConfigureJson(this IFluxRestServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure(builder.ServiceOptions.SerializerOptions);

        return builder;
    }
}
