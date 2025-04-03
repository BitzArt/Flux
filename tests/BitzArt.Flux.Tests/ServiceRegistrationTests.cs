using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class ServiceRegistrationTests
{
    [Fact]
    public void AddFlux_WithEmptyConfiguration_ShouldAddEmptyFluxServiceFactory()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddFlux(x => { });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetService<IFluxFactory>();

        Assert.NotNull(factory);
        Assert.NotNull(factory.ServiceRegistrations);
        Assert.Empty(factory.ServiceRegistrations);
    }

    [Fact]
    public void AddFlux_Twice_ShouldThrowOnSecondAdd()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddFlux(x => { });

        // Assert
        Assert.ThrowsAny<Exception>(() => services.AddFlux(x => { }));
    }

    [Fact]
    public void AddFlux_Empty_ShouldAddFluxContext()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddFlux(x => { });

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        var context = serviceProvider.GetService<IFluxContext>();
        Assert.NotNull(context);
    }

    private class MyModel { }

    private void Test()
    {
        FluxSetContext<MyModel, int> setContext = null!;

        IEnumerable<KeyValuePair<string, object>> parameters =
            [
                new("a", 1),
                new("b", 2),
                new("c", 3)
            ];

        _ = setContext.GetAsync(1, new(1, 2, 3));
        _ = setContext.GetAsync(1, new(parameters));
    }
}