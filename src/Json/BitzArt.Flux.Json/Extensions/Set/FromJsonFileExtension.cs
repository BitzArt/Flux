using System.Text.Json;

namespace BitzArt.Flux.Json;

/// <summary>
/// Extension methods for configuring a set from a JSON dataset file.
/// </summary>
public static class FromJsonFileExtension
{
    /// <summary>
    /// Configures the set to use a provided JSON dataset from the specified file.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type of the set.
    /// </typeparam>
    /// <param name="builder"></param>
    /// <param name="filePath">
    /// The path to the JSON file containing the dataset.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel> FromJsonFile<TModel>(this IFluxJsonSetBuilder<TModel> builder,
        string filePath)
        where TModel : class
    {
        var path = GetFilePath(filePath, builder.ServiceConfiguration.BaseFilePath);
        var items = TryGetItemsFromJsonFile<TModel>(path, builder.ServiceConfiguration.JsonSerializerOptions);
        builder.SetConfiguration.DataCollection = new(items);

        return builder;
    }

    private static List<TModel> TryGetItemsFromJsonFile<TModel>(string path, JsonSerializerOptions options)
        => GetItemsFromJsonFile<TModel>(path, options);

    private static List<TModel> GetItemsFromJsonFile<TModel>(string path, JsonSerializerOptions options)
    {
        var jsonString = File.ReadAllText(path);
        var items = JsonSerializer.Deserialize<List<TModel>>(jsonString, options)!;

        return items;
    }

    private static string GetFilePath(string filePath, string? basePath = null)
    {
        if (basePath is not null) filePath = Path.Combine(basePath, filePath);

        return filePath;
    }
}