using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BitzArt.Flux;

file class TestEntity
{
    public int Id { get; set; }
}

public class ServiceRegistrationTests
{
    [Fact]
    public void UsingRest_WithEntity_AddsFactoryAndEntityContext()
    {
        var services = new ServiceCollection();

        var serviceName = "SomeExternalService";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
            .UsingRest("http://localhost")
            .AddEntity<TestEntity, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IFluxProvider>();
        Assert.NotNull(factory);
        Assert.True(factory.ServiceContexts.Count > 0);

        Assert.True(factory.ServiceContexts.Count == 1);
        var provider = factory.ServiceContexts.Single();

        Assert.Equal(serviceName, provider.ServiceName);

        var entityContext = serviceProvider.GetRequiredService<IFluxEntityContext<TestEntity, int>>();
        Assert.NotNull(entityContext);
    }

    [Fact]
    public void UsingRest_ConfigureHttpClient_ConfiguresHttpClient()
    {
        var services = new ServiceCollection();

        var testHeader = new KeyValuePair<string, string>("TestHeader", "TestValue");

        services.AddFlux(flux =>
        {
            flux.AddService("test")
            .UsingRest("http://localhost")
            .ConfigureHttpClient(client =>
            {
                client.DefaultRequestHeaders.Add(testHeader.Key, testHeader.Value);
            })
            .AddEntity<TestEntity, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();
        var entityContext = serviceProvider.GetRequiredService<IFluxEntityContext<TestEntity, int>>();
        Assert.NotNull(entityContext);

        var entityContextCasted = (FluxRestEntityContext<TestEntity, int>)entityContext;

        Assert.Contains(entityContextCasted.HttpClient.DefaultRequestHeaders,
            x => x.Key == testHeader.Key && x.Value.Single() == testHeader.Value);
    }

    [Fact]
    public void UsingRest_WithJsonOptions_ConfiguresJson()
    {
        var services = new ServiceCollection();

        bool configUsed = false;

        services.AddFlux(flux =>
        {
            flux.AddService("test")
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
        var entityContext = serviceProvider.GetRequiredService<IFluxEntityContext<TestEntity, int>>();
        Assert.NotNull(entityContext);

        var entityContextCasted = (FluxRestEntityContext<TestEntity, int>)entityContext;
        var serializerOptions = entityContextCasted.ServiceOptions.SerializerOptions;

        Assert.True(serializerOptions.WriteIndented);
        Assert.Equal(JsonIgnoreCondition.WhenWritingNull, serializerOptions.DefaultIgnoreCondition);
    }

    [Fact]
    public void UsingRest_WithService_AddsServiceContext()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("Service1")
            .UsingRest("http://localhost")
            .AddEntity<TestEntity>("test-entity");
        });

        var serviceProvider = services.BuildServiceProvider();

        var flux = serviceProvider.GetRequiredService<IFluxContext>();
    }

    [Fact]
    public void AddFlux_GetAllPackageSignatureElementsFromFluxContext_ReturnsAll()
    {
        var services = new ServiceCollection();
        services.AddFlux(flux =>
        {
            flux.AddService("service")
            .UsingRest("http://localhost")
                .AddEntity<TestEntity>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var fluxContext = serviceProvider.GetRequiredService<IFluxContext>();

        var service = fluxContext.Service("service");
        Assert.NotNull(service);
        Assert.True(service is FluxServiceContext);

        var provider = ((FluxServiceContext)service).Provider;
        Assert.True(provider is FluxRestServiceProvider);

        var entity = service.Entity<TestEntity>();
        Assert.NotNull(entity);
        Assert.True(entity is FluxRestEntityContext<TestEntity>);
    }

    [Fact]
    public void AddFlux_GetAllPackageSignatureElementsFromDiContainer_ReturnsAll()
    {
        var services = new ServiceCollection();
        services.AddFlux(flux =>
        {
            flux.AddService("service1")
            .UsingRest("http://localhost1")
                .AddEntity<TestEntity>("test");

            flux.AddService("service2")
            .UsingRest("http://localhost2")
                .AddEntity<TestEntity>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var serviceContexts = serviceProvider.GetRequiredService<IEnumerable<IFluxServiceContext>>();
        Assert.NotNull(serviceContexts);
        Assert.True(serviceContexts.Any());
        Assert.True(serviceContexts.Count() == 2);
    }
}