using BitzArt.Flux.Builder;
using BitzArt.Flux.Sets;
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
        var serviceBuilder = fluxBuilder.AddService("my-flux-service");

        // Assert
        Assert.NotNull(serviceBuilder);
        Assert.IsType<FluxServiceProtocolConfigurator>(serviceBuilder);
    }

    [Fact]
    public void AddService_OnEmptyFluxBuilder_ShouldRegisterKeyedServiceForInternalConsumption()
    {
        // Arrange
        var services = new ServiceCollection();

        var serviceName = "my-flux-service";
        var fluxBuilder = new FluxBuilder(services);

        // Act
        fluxBuilder.AddService(serviceName);

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        var serviceSignature = new FluxServiceSignature(serviceName);
        var registeredServiceContext = serviceProvider.GetKeyedService<IFluxServiceContext>(serviceSignature);

        Assert.NotNull(registeredServiceContext);
        Assert.IsType<FluxServiceContext>(registeredServiceContext);
        Assert.Equal(serviceName, registeredServiceContext.ServiceName);
    }

    [Fact]
    public void AddService_OnEmptyFluxBuilder_ShouldAddExternalRegistrationForArbitraryInjection()
    {
        // Arrange
        var services = new ServiceCollection();

        var serviceName = "my-flux-service";
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

    [Fact]
    public void AddService_AfterAnotherService_ShouldAddBoth()
    {
        // Arrange
        var services = new ServiceCollection();

        var serviceName1 = "my-flux-service-1";
        var serviceName2 = "my-flux-service-2";

        var fluxBuilder = new FluxBuilder(services);

        // Act
        fluxBuilder.AddService(serviceName1);
        fluxBuilder.AddService(serviceName2);

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var registeredServiceContexts = serviceProvider.GetService<IEnumerable<IFluxServiceContext>>();
        Assert.NotNull(registeredServiceContexts);

        Assert.Equal(2, registeredServiceContexts.Count());
        var serviceContext1 = registeredServiceContexts.First(x => x.ServiceName == serviceName1);
        var serviceContext2 = registeredServiceContexts.First(x => x.ServiceName == serviceName2);

        Assert.IsType<FluxServiceContext>(serviceContext1);
        Assert.IsType<FluxServiceContext>(serviceContext2);

        Assert.Equal(serviceName1, serviceContext1.ServiceName);
        Assert.Equal(serviceName2, serviceContext2.ServiceName);
    }
}