using Microsoft.Extensions.DependencyInjection;

namespace Flex;

public class ServiceRegistrationTests
{
    [Fact]
    public void AddCommunicator_WithEmptyConfiguration_AddsEmptyCommunicatorFactory()
    {
        var services = new ServiceCollection();

        services.AddCommunicator(x => { });

        var serviceProvider = services.BuildServiceProvider();
        var factory = serviceProvider.GetService<ICommunicatorServiceFactory>();

        Assert.NotNull(factory);
        Assert.NotNull(factory.Providers);
        Assert.Empty(factory.Providers);
    }

    [Fact]
    public void AddCommunicator_Twice_ThrowsOnSecondAdd()
    {
        var services = new ServiceCollection();

        services.AddCommunicator(x => { });

        Assert.ThrowsAny<Exception>(() => services.AddCommunicator(x => { }));
    }
    
    [Fact]
    public void AddCommunicator_Empty_AddsCommunicationContext()
    {
        var services = new ServiceCollection();
        services.AddCommunicator(x => { });
        var serviceProvider = services.BuildServiceProvider();

        var communicationContext = serviceProvider.GetService<ICommunicationContext>();
        Assert.NotNull(communicationContext);

        var communicationContextByDirectType = serviceProvider.GetService<CommunicationContext>();
        Assert.Null(communicationContextByDirectType);
    }
}