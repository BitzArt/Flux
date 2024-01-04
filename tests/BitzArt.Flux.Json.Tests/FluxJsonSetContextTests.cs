using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class FluxJsonSetContextTests
{
    [Fact]
    public async Task UsingJson_WithBasePath_ReadsJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson("Data")
                .AddSet<TestModel>("test-model.set.json").WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task UsingJson_BasePathDirectlyInSetStartingWithDot_ReadsJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                .AddSet<TestModel>("./Data/test-model.set.json").WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task UsingJson_BasePathDirectlyInSetStartingWithNoDot_ReadsJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                .AddSet<TestModel>("Data/test-model.set.json").WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }

    [Fact]
    public async Task UsingJson_BasePathGlobalByGettingCurrentDirectory_ReadsJsonData()
    {
        var services = new ServiceCollection();

        var currentDirectory = Directory.GetCurrentDirectory();
        var dataDirectory = $"{currentDirectory.TrimEnd('\\').TrimEnd('/')}/Data";

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson(dataDirectory)
                .AddSet<TestModel>("test-model.set.json").WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
    }
}