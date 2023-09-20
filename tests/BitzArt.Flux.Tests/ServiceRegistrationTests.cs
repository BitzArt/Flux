using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class ServiceRegistrationTests
{
    [Fact]
    public void AddFlux_WithEmptyConfiguration_AddsEmptyFluxServiceFactory()
    {
        var services = new ServiceCollection();

        services.AddFlux(x => { });

        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetService<IFluxServiceFactory>();

        Assert.NotNull(factory);
        Assert.NotNull(factory.ServiceContexts);
        Assert.Empty(factory.ServiceContexts);
    }

    [Fact]
    public void AddFlux_Twice_ThrowsOnSecondAdd()
    {
        var services = new ServiceCollection();

        services.AddFlux(x => { });

        Assert.ThrowsAny<Exception>(() => services.AddFlux(x => { }));
    }
    
    [Fact]
    public void AddFlux_Empty_AddsCommunicationContext()
    {
        var services = new ServiceCollection();
        services.AddFlux(x => { });
        var serviceProvider = services.BuildServiceProvider();

        var communicationContext = serviceProvider.GetService<IFlux>();
        Assert.NotNull(communicationContext);

        var communicationContextByDirectType = serviceProvider.GetService<Flux>();
        Assert.Null(communicationContextByDirectType);
    }
}