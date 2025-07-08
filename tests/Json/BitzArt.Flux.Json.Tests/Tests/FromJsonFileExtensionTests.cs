using BitzArt.Flux.Json;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class FromJsonFileExtensionTests
{
    [Fact]
    public async Task FromJsonFile_WithBasePath_ShouldReadJsonData()
    {
        // Arrange
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

        // Act
        var data = await setContext.GetAllAsync();

        // Assert
        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathDirectlyInSetStartingWithDot_ShouldReadJsonData()
    {
        // Arrange
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

        // Act
        var data = await setContext.GetAllAsync();

        // Assert
        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathDirectlyInSetStartingWithNoDot_ShouldReadJsonData()
    {
        // Arrange
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

        // Act
        var data = await setContext.GetAllAsync();

        // Assert
        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathGlobalByGettingCurrentDirectory_ShouldReadJsonData()
    {
        // Arrange
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

        // Act
        var data = await setContext.GetAllAsync();

        // Assert
        Assert.NotNull(data);
        Assert.True(data.Any());
    }
}