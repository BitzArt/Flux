using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

internal static class TestSetContext
{
    public static IFluxSetContext<TestModel> GetTestSetContext()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        
        const string serviceName = "JsonService";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson("Data", json =>
                {
                    json.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    json.Converters.Add(new JsonStringEnumConverter());
                    json.WriteIndented = true;
                })
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();
    }
}