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
        Assert.NotNull(factory.ServiceContexts);
        Assert.Empty(factory.ServiceContexts);
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
}