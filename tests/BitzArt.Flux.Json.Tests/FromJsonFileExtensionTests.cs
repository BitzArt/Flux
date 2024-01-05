using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class FromJsonFileExtensionTests
{
    [Fact]
    public async Task FromJsonFile_WithBasePath_ReadsJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .AddSet<TestModel>()
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathDirectlyInSetStartingWithDot_ReadsJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                .AddSet<TestModel>()
                    .FromJsonFile("./Data/test-model.set.json")
                    .WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathDirectlyInSetStartingWithNoDot_ReadsJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .AddSet<TestModel>()
                        .FromJsonFile("Data/test-model.set.json")
                        .WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task FromJsonFile_BasePathGlobalByGettingCurrentDirectory_ReadsJsonData()
    {
        var services = new ServiceCollection();

        var currentDirectory = Directory.GetCurrentDirectory();
        var dataDirectory = $"{currentDirectory.TrimEnd('\\').TrimEnd('/')}/Data";

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .WithBaseFilePath(dataDirectory)
                    .AddSet<TestModel>()
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }
}