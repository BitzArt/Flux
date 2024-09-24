using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal static class TestSetContext
{
    public static IFluxSetContext<TestModel> GetTestSetContext()
    {
        var services = new ServiceCollection();
        
        const string serviceName = "JsonService";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .ConfigureJson(json =>
                    {
                        json.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                        json.Converters.Add(new JsonStringEnumConverter());
                        json.WriteIndented = true;
                    })
                    .AddSet<TestModel, int>()
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();
    }
}