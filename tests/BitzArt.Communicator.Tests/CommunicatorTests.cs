using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Communicator.Tests;

public class CommunicatorTests
{
    [Fact]
    public void AddCommunicator_WithService_AddsCommunicators()
    {
        var services = new ServiceCollection();

        services.AddCommunicator(x =>
            x.AddService("SomeExternalService")
            .UsingRest()
            .AddEntity<TestEntity, int>("test"));
    }
}