using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BitzArt.Communicator;

file class TestEntity
{
    public int Id { get; set; }
}

public class ServiceRegistrationTests
{
    [Fact]
    public void UsingRest_WithEntity_AddsFactoryAndEntityCommunicator()
    {
        var services = new ServiceCollection();

        var serviceName = "SomeExternalService";

        services.AddCommunicator(communicator =>
        {
            communicator.AddService(serviceName)
            .UsingRest("http://localhost")
            .AddEntity<TestEntity, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<ICommunicatorServiceFactory>();
        Assert.NotNull(factory);
        Assert.True(factory.Providers.Count > 0);

        Assert.True(factory.Providers.Count == 1);
        var provider = factory.Providers.Single();

        Assert.Equal(serviceName, provider.ServiceName);

        var entityCommunicator = serviceProvider.GetRequiredService<ICommunicationContext<TestEntity, int>>();
        Assert.NotNull(entityCommunicator);
    }

    [Fact]
    public void UsingRest_ConfigureHttpClient_ConfiguresHttpClient()
    {
        var services = new ServiceCollection();

        var testHeader = new KeyValuePair<string, string>("TestHeader", "TestValue");

        services.AddCommunicator(communicator =>
        {
            communicator.AddService("test")
            .UsingRest("http://localhost")
            .ConfigureHttpClient(client =>
            {
                client.DefaultRequestHeaders.Add(testHeader.Key, testHeader.Value);
            })
            .AddEntity<TestEntity, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();
        var entityCommunicator = serviceProvider.GetRequiredService<ICommunicationContext<TestEntity, int>>();
        Assert.NotNull(entityCommunicator);

        var communicatorCasted = (CommunicatorRestEntityContext<TestEntity, int>)entityCommunicator;

        Assert.Contains(communicatorCasted.HttpClient.DefaultRequestHeaders,
            x => x.Key == testHeader.Key && x.Value.Single() == testHeader.Value);
    }

    [Fact]
    public void UsingRest_WithJsonOptions_ConfiguresJson()
    {
        var services = new ServiceCollection();

        bool configUsed = false;

        services.AddCommunicator(communicator =>
        {
            communicator.AddService("test")
            .UsingRest("http://localhost")
            .ConfigureJson(x =>
            {
                configUsed = true;

                x.WriteIndented = true;
                x.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            })
            .AddEntity<TestEntity, int>("test");
        });

        Assert.True(configUsed);

        var serviceProvider = services.BuildServiceProvider();
        var entityCommunicator = serviceProvider.GetRequiredService<ICommunicationContext<TestEntity, int>>();
        Assert.NotNull(entityCommunicator);

        var communicatorCasted = (CommunicatorRestEntityContext<TestEntity, int>)entityCommunicator;
        var serializerOptions = communicatorCasted.ServiceOptions.SerializerOptions;

        Assert.True(serializerOptions.WriteIndented);
        Assert.Equal(JsonIgnoreCondition.WhenWritingNull, serializerOptions.DefaultIgnoreCondition);
    }
}