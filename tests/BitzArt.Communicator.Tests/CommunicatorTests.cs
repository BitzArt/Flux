using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator.Tests;

public class CommunicatorTests
{
    [Fact]
    public void AddCommunicator_WithService_AddsCommunicators()
    {
        var services = new ServiceCollection();

        services.AddCommunicator(x =>
        {
            x.AddService("SomeExternalService")
            .UsingRest("http://localhost")
            .AddEntity<TestEntity, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<ICommunicatorServiceFactory>();
        Assert.NotNull(factory);
        Assert.True(factory.Providers.Count > 0);

        var entityCommunicator = serviceProvider.GetRequiredService<IEntityCommunicator<TestEntity, int>>();
        Assert.NotNull(entityCommunicator);
    }
}