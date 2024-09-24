using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class FromJsonExtensionTests
{
    private string jsonData =
        """
        [
            {
              "id": 1
            },
            {
              "id": 2
            }
        ]
        """;

    [Fact]
    public async Task FromJson_ValidJson_ReadsJsonData()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .AddSet<TestModel>()
                        .FromJson(jsonData)
                        .WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        var data = await setContext.GetAllAsync();

        Assert.NotNull(data);
        Assert.True(data.Any());
        Assert.Equal(2, data.Count());

        Assert.Contains(data, x => x.Id == 1);
        Assert.Contains(data, x => x.Id == 2);
    }

    [Fact]
    public void FromJson_EmptyString_ThrowsOnAddingSet()
    {
        var services = new ServiceCollection();

        Assert.ThrowsAny<Exception>(() =>
        {
            services.AddFlux(flux =>
            {
                flux.AddService("service 1")
                    .UsingJson()
                        .AddSet<TestModel>()
                            .FromJson(string.Empty)
                            .WithKey(x => x.Id!);
            });
        });
    }

    [Fact]
    public void FromJson_Null_ThrowsOnAddingSet()
    {
        var services = new ServiceCollection();

        Assert.ThrowsAny<Exception>(() =>
        {
            services.AddFlux(flux =>
            {
                flux.AddService("service 1")
                    .UsingJson()
                        .AddSet<TestModel>()
                            .FromJson(null!)
                            .WithKey(x => x.Id!);
            });
        });
    }
}