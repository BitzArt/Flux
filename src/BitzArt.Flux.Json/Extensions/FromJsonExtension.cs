using System.Text.Json;

namespace BitzArt.Flux;

public static class FromJsonExtension
{
    public static IFluxJsonSetBuilder<TModel> FromJson<TModel>(this IFluxJsonSetBuilder<TModel> builder,
        string json)
        where TModel : class
    {
        builder.SetOptions.Items =
            JsonSerializer.Deserialize<List<TModel>>(json, builder.ServiceOptions.SerializerOptions)
            ?? throw new FluxJsonDeserializationException<List<TModel>>();

        return builder;
    }

    public static IFluxJsonSetBuilder<TModel, TKey> FromJson<TModel, TKey>(this IFluxJsonSetBuilder<TModel, TKey> builder,
        string json)
        where TModel : class
    {
        builder.SetOptions.Items =
            JsonSerializer.Deserialize<List<TModel>>(json, builder.ServiceOptions.SerializerOptions)
            ?? throw new FluxJsonDeserializationException<List<TModel>>();

        return builder;
    }
}