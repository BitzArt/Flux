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
    /// <param name="builder"></param>
    /// <param name="json">
    /// JSON string containing the dataset.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel> FromJson<TModel>(this IFluxJsonSetBuilder<TModel> builder,
        string json)
        where TModel : class
    {
        var items = JsonSerializer.Deserialize<List<TModel>>(json, builder.ServiceConfiguration.JsonSerializerOptions)!;
        builder.SetConfiguration.DataCollection = new(items);

        return builder;
    }
}
