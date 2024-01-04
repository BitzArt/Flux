using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddSetExtension
{
    public static IFluxJsonSetBuilder<TModel> AddSet<TModel>(this IFluxJsonServiceBuilder serviceBuilder,
        string filePath, string? name = null)
        where TModel : class
    {
        var builder = new FluxJsonSetBuilder<TModel>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceFactory = builder.ServiceFactory;

        var path = GetFilePath(filePath, serviceBuilder.BasePath);
        builder.SetOptions.Items = TryGetItemsFromJsonFile<TModel>(path, serviceBuilder.ServiceOptions.SerializerOptions);

        serviceFactory.AddSet<TModel>(builder.SetOptions, name);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxFactory>();
            return factory.GetSetContext<TModel>(x, serviceFactory.ServiceName);
        });

        return builder;
    }

    public static IFluxJsonSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxJsonServiceBuilder serviceBuilder,
        string filePath, string? name = null)
        where TModel : class
    {
        var builder = new FluxJsonSetBuilder<TModel, TKey>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceFactory = serviceBuilder.ServiceFactory;

        var path = GetFilePath(filePath, serviceBuilder.BasePath);
        builder.SetOptions.Items = TryGetItemsFromJsonFile<TModel>(path, serviceBuilder.ServiceOptions.SerializerOptions);

        serviceFactory.AddSet<TModel, TKey>(builder.SetOptions, name);

        services.AddScoped(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetSetContext<TModel, TKey>(x, serviceFactory.ServiceName);
        });

        services.AddScoped<IFluxSetContext<TModel>>(x =>
        {
            var provider = x.GetRequiredService<IFluxFactory>();
            return provider.GetSetContext<TModel, TKey>(x, serviceFactory.ServiceName);
        });

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
            throw new JsonFileReadException(path, ex);
        }
    }

    private static ICollection<TModel> GetItemsFromJsonFile<TModel>(string path, JsonSerializerOptions options)
    {
        var jsonString = File.ReadAllText(path);
        var items = JsonSerializer.Deserialize<List<TModel>>(jsonString, options);

        if (items is null)
            throw new JsonDeserializationException<List<TModel>>();

        return items;
    }

    private static string GetFilePath(string filePath, string? basePath = null)
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        if (basePath is not null) filePath = Path.Combine(basePath, filePath);

        return Path.Combine(currentDirectory, filePath);
    }
}

internal class JsonFileReadException : Exception
{
    public JsonFileReadException(string path, Exception innerException)
        : base($"Error reading JSON from file '{path}'. See inner exception for details", innerException)
    { }
}
    
internal class JsonDeserializationException<TModel> : Exception
{
    public JsonDeserializationException()
        : base($"Failed to deserialize JSON to {typeof(TModel).Name}")
    { }
}