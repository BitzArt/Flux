using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.Json;

public class FromJsonExtensionTests
{
    private readonly string jsonData =
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
    public async Task FromJson_ValidJson_ShouldReadJsonData()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .AddSet<TestModel>()
                        .FromJson(jsonData)
                        .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        // Act
        var data = await setContext.GetAllAsync();

        // Assert
        Assert.NotNull(data);
        Assert.True(data.Any());
        Assert.Equal(2, data.Count());

        Assert.Contains(data, x => x.Id == 1);
        Assert.Contains(data, x => x.Id == 2);
    }

    [Fact]
    public void FromJson_EmptyString_ShouldThrowOnAddingSet()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act/Assert
        Assert.ThrowsAny<Exception>(() =>
        {
            services.AddFlux(flux =>
            {
                flux.AddService("service 1")
                    .UsingJson()
                        .AddSet<TestModel>()
                            .FromJson(string.Empty)
                            .WithKey(x => x.Id!.Value);
            });
        });
    }

    [Fact]
    public void FromJson_Null_ShouldThrowOnAddingSet()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act/Assert
        Assert.ThrowsAny<Exception>(() =>
        {
            services.AddFlux(flux =>
            {
                flux.AddService("service 1")
                    .UsingJson()
                        .AddSet<TestModel>()
                            .FromJson(null!)
                            .WithKey(x => x.Id!.Value);
            });
        });
    }
}