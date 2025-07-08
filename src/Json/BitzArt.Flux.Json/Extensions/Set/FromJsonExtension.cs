using System.Text.Json;

namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for configuring a set from a JSON dataset string.
/// </summary>
public static class FromJsonExtension
{
    /// <summary>
    /// Configures the set to use a provided JSON dataset.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type of the set.
    /// </typeparam>
    /// <typeparam name="TKey">
    /// The key type of the set.
    /// </typeparam>
    /// <param name="builder"></param>
    /// <param name="json">
    /// JSON string containing the dataset.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel,TKey}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel, TKey> FromJson<TModel, TKey>(this IFluxJsonSetBuilder<TModel, TKey> builder,
        string json)
        where TModel : class
    {
        builder.SetConfiguration.DataCollection.Items =
            JsonSerializer.Deserialize<List<TModel>>(json, builder.ServiceConfiguration.JsonSerializerOptions)
            ?? throw new FluxJsonDeserializationException<List<TModel>>();

        return builder;
    }
}
