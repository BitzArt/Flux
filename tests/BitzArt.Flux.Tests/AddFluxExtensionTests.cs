using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class AddFluxExtensionTests
{
    [Fact]
    public void AddFlux_OnEmptyServiceCollection_ShouldAddFluxContext()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddFlux(_ => { });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var fluxContext = serviceProvider.GetService<IFluxContext>();
        Assert.NotNull(fluxContext);
    }

    [Fact]
    public void AddFlux_OnAlreadyRegistered_ShouldRetainFluxContext()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddFlux(_ => { });

        // Act
        services.AddFlux(_ => { });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var fluxContext = serviceProvider.GetService<IFluxContext>();
        Assert.NotNull(fluxContext);
    }
}
