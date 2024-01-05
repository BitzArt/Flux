using System.Text.Json;

namespace BitzArt.Flux;

public static class ConfigureJsonExtension
{
    public static IFluxJsonServiceBuilder ConfigureJson(this IFluxJsonServiceBuilder builder, Action<JsonSerializerOptions> configure)
    {
        configure(builder.ServiceOptions.SerializerOptions);

        return builder;
    }
}
