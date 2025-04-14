using BitzArt.Flux.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class AddServiceExtensionTests
{
    [Fact]
    public void AddService_OnFluxBuilder_ShouldReturnBuilder()
    {
        // Arrange
        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);

        // Act
        var serviceBuilder = fluxBuilder.AddService("TestService");

        // Assert
        Assert.NotNull(serviceBuilder);
        Assert.IsType<FluxServiceBuilder>(serviceBuilder);
    }

    [Fact]
    public void AddService_OnEmptyFluxBuilder_ShouldAddService()
    {
        // Arrange
        var serviceName = "TestService";
        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);

        // Act
        fluxBuilder.AddService(serviceName);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var registeredServiceContexts = serviceProvider.GetService<IEnumerable<IFluxServiceContext>>();
        Assert.NotNull(registeredServiceContexts);
        Assert.Single(registeredServiceContexts);

        var serviceContext = registeredServiceContexts.First();
        Assert.Equal(serviceName, serviceContext.ServiceName);
        Assert.IsType<FluxServiceContext>(serviceContext);
    }
}