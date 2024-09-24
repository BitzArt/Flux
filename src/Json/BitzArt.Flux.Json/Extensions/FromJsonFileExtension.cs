using System.Text.Json;

namespace BitzArt.Flux;

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
        var path = GetFilePath(filePath, builder.ServiceOptions.BaseFilePath);
        builder.SetOptions.Items = TryGetItemsFromJsonFile<TModel>(path, builder.ServiceOptions.SerializerOptions);

        return builder;
    }

    /// <summary>
    /// Configures the set to use a provided JSON dataset from the specified file.
    /// </summary>
    /// <typeparam name="TModel">
    /// The model type of the set.
    /// </typeparam>
    /// <typeparam name="TKey">
    /// The key type of the set.
    /// </typeparam>
    /// <param name="builder"></param>
    /// <param name="filePath">
    /// The path to the JSON file containing the dataset.
    /// </param>
    /// <returns>
    /// The <see cref="IFluxJsonSetBuilder{TModel,TKey}"/> for further set configuration.
    /// </returns>
    public static IFluxJsonSetBuilder<TModel, TKey> FromJsonFile<TModel, TKey>(this IFluxJsonSetBuilder<TModel, TKey> builder,
        string filePath)
        where TModel : class
    {
        var path = GetFilePath(filePath, builder.ServiceOptions.BaseFilePath);
        builder.SetOptions.Items = TryGetItemsFromJsonFile<TModel>(path, builder.ServiceOptions.SerializerOptions);

        return builder;
    }

    private static List<TModel> TryGetItemsFromJsonFile<TModel>(string path, JsonSerializerOptions options)
    {
        try
        {
            return GetItemsFromJsonFile<TModel>(path, options);
        }
        catch (Exception ex)
        {
            throw new FluxJsonFileReadException(path, ex);
        }
    }

    private static List<TModel> GetItemsFromJsonFile<TModel>(string path, JsonSerializerOptions options)
    {
        var jsonString = File.ReadAllText(path);
        var items = JsonSerializer.Deserialize<List<TModel>>(jsonString, options)
            ?? throw new FluxJsonDeserializationException<List<TModel>>();

        return items;
    }

    private static string GetFilePath(string filePath, string? basePath = null)
    {
        if (basePath is not null) filePath = Path.Combine(basePath, filePath);

        return filePath;
    }
}
