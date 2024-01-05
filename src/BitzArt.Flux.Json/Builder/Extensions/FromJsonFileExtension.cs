using System.Text.Json;

namespace BitzArt.Flux;

public static class FromJsonFileExtension
{
    public static IFluxJsonSetBuilder<TModel> FromJsonFile<TModel>(this IFluxJsonSetBuilder<TModel> builder,
        string filePath)
        where TModel : class
    {
        var path = GetFilePath(filePath, builder.ServiceOptions.BaseFilePath);
        builder.SetOptions.Items = TryGetItemsFromJsonFile<TModel>(path, builder.ServiceOptions.SerializerOptions);

        return builder;
    }

    public static IFluxJsonSetBuilder<TModel, TKey> FromJsonFile<TModel, TKey>(this IFluxJsonSetBuilder<TModel, TKey> builder,
        string filePath)
        where TModel : class
    {
        var path = GetFilePath(filePath, builder.ServiceOptions.BaseFilePath);
        builder.SetOptions.Items = TryGetItemsFromJsonFile<TModel>(path, builder.ServiceOptions.SerializerOptions);

        return builder;
    }

    private static ICollection<TModel> TryGetItemsFromJsonFile<TModel>(string path, JsonSerializerOptions options)
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

    private static ICollection<TModel> GetItemsFromJsonFile<TModel>(string path, JsonSerializerOptions options)
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

internal class FluxJsonFileReadException : Exception
{
    public FluxJsonFileReadException(string path, Exception innerException)
        : base($"Error reading JSON from file '{path}'. See inner exception for details", innerException)
    { }
}
    
internal class FluxJsonDeserializationException<TModel> : Exception
{
    public FluxJsonDeserializationException()
        : base($"Failed to deserialize JSON to {typeof(TModel).Name}")
    { }
}