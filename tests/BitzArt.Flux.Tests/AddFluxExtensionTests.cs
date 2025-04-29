using Microsoft.Extensions.DependencyInjection;
using System;

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

    [Fact]
    public void AddFlux_WithAnUnterminatedService_ShouldThrow()
    {
        // A service builder becomes "terminated" after it was finalized
        // by a Flux implementation, e.g. `.UsingRest()` or `.UsingJson()`.

        // Arrange
        var services = new ServiceCollection();

        // Act + Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            services.AddFlux(flux =>
            {
                flux.AddService("my-unfinalized-flux-service");
            });
        });
    }
}
