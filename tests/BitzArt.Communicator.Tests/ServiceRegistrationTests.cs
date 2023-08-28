using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BitzArt.Communicator;

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
    public void AddCommunicator_EmptyAndTwice_AddsEmptyFactoryTwice()
    {
        var services = new ServiceCollection();

        services.AddCommunicator(x => { });
        services.AddCommunicator(x => { });

        var serviceProvider = services.BuildServiceProvider();
        var factories = serviceProvider.GetService<IEnumerable<ICommunicatorServiceFactory>>();

        Assert.NotNull(factories);
        Assert.True(factories.Count() == 2);

        foreach (var factory in factories) Assert.Empty(factory.Providers);
    }
}