using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddSetExtension
{
    public static IFluxJsonSetBuilder<TModel> AddSet<TModel>(this IFluxJsonServiceBuilder serviceBuilder, string filePath, string? name = null)
        where TModel : class
    {
        var builder = new FluxJsonSetBuilder<TModel>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceFactory = builder.ServiceFactory;
        
        builder.SetOptions.Items = GetItemsFromJsonFile<TModel>(filePath, serviceBuilder.BasePath);

        serviceFactory.AddSet<TModel>(builder.SetOptions, name);

        services.AddScoped(x =>
        {
            var factory = x.GetRequiredService<IFluxFactory>();
            return factory.GetSetContext<TModel>(x, serviceFactory.ServiceName);
        });

        return builder;
    }
    
    public static IFluxJsonSetBuilder<TModel, TKey> AddSet<TModel, TKey>(this IFluxJsonServiceBuilder serviceBuilder, string filePath, string? name = null)
        where TModel : class
    {
        var builder = new FluxJsonSetBuilder<TModel, TKey>(serviceBuilder);

        var services = serviceBuilder.Services;
        var serviceFactory = serviceBuilder.ServiceFactory;

        builder.SetOptions.Items = GetItemsFromJsonFile<TModel>(filePath, serviceBuilder.BasePath);
        
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
    
    private static ICollection<TModel> GetItemsFromJsonFile<TModel>(string filePath, string? basePath = null)
    {
        try
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            if (basePath is not null) filePath = Path.Combine(basePath, filePath);
            
            var path = Path.Combine(currentDirectory, filePath);
            
            var jsonString = File.ReadAllText(path);
            var items = JsonSerializer.Deserialize<List<TModel>>(jsonString);

            if (items is null)
                throw new Exception($"Failed to deserialize JSON from ${path} to {typeof(List<TModel>)}");

            return items;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading JSON file, see inner exception for details", ex);
        }
    }
}