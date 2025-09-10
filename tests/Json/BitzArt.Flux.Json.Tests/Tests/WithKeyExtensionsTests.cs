using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.Json;

public class WithKeyExtensionsTests
{
    [Fact]
    public void WithKey_ValidConditions_GetByIdReturnsResult()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson("Data")
                    .AddSet<TestModel>()
                    .FromJsonFile("test-model.set.json")
                    .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        // Act
        var result = setContext.GetAsync(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task WithKey_WhenNotCalled_GetByIdShouldThrowAsync()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson("Data")
                    .AddSet<TestModel>()
                    .FromJsonFile("test-model.set.json");
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        // Act/Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            var result = await setContext.GetAsync(1);
        });
    }

    [Fact]
    public void WithKey_WhenItemMissesKeyProperty_ShouldThrow()
    {
        var data = """
        [
            {
              "id": 1
            },
            {
              "id": 2
            },
            {
              "name": "Item without id property"
            }
        ]
        """;

        var services = new ServiceCollection();

        // Act/Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            services.AddFlux(flux =>
            {
                flux.AddService("service 1")
                    .UsingJson()
                        .AddSet<TestModel>()
                        .FromJson(data)
                        .WithKey(x => x.Id!.Value);
            });
        });
    }
}