using BitzArt.Flux.Json;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class FromJsonFileExtensionTests
{
    [Fact]
    public async Task FromJsonFile_WithBasePath_ShouldReadJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson("Data")
                    .AddSet<TestModel, int>()
                    .FromJsonFile("test-model.set.json")
                    .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathDirectlyInSetStartingWithDot_ShouldReadJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .AddSet<TestModel, int>()
                    .FromJsonFile("./Data/test-model.set.json")
                    .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathDirectlyInSetStartingWithNoDot_ShouldReadJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .AddSet<TestModel, int>()
                    .FromJsonFile("Data/test-model.set.json")
                    .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathGlobalByGettingCurrentDirectory_ShouldReadJsonData()
    {
        var services = new ServiceCollection();

        var currentDirectory = Directory.GetCurrentDirectory();
        var dataDirectory = $"{currentDirectory.TrimEnd('\\').TrimEnd('/')}/Data";

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson(dataDirectory)
                    .AddSet<TestModel, int>()
                    .FromJsonFile("test-model.set.json")
                    .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }
}