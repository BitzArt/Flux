using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BitzArt.Flux;

file class TestModel
{
    public int Id { get; set; }
}

public class ServiceRegistrationTests
{
    [Fact]
    public void UsingRest_WithModel_AddsFactoryAndModelContext()
    {
        var services = new ServiceCollection();

        var serviceName = "SomeExternalService";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
            .UsingRest("http://localhost")
            .AddModel<TestModel, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IFluxFactory>();
        Assert.NotNull(factory);
        Assert.True(factory.ServiceContexts.Count > 0);

        Assert.True(factory.ServiceContexts.Count == 1);
        var provider = factory.ServiceContexts.Single();

        Assert.Equal(serviceName, provider.ServiceName);

        var modelContext = serviceProvider.GetRequiredService<IFluxModelContext<TestModel, int>>();
        Assert.NotNull(modelContext);
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
            .AddModel<TestModel, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();
        var modelContext = serviceProvider.GetRequiredService<IFluxModelContext<TestModel, int>>();
        Assert.NotNull(modelContext);

        var modelContextCasted = (FluxRestModelContext<TestModel, int>)modelContext;

        Assert.Contains(modelContextCasted.HttpClient.DefaultRequestHeaders,
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
            .AddModel<TestModel, int>("test");
        });

        Assert.True(configUsed);

        var serviceProvider = services.BuildServiceProvider();
        var modelContext = serviceProvider.GetRequiredService<IFluxModelContext<TestModel, int>>();
        Assert.NotNull(modelContext);

        var modelContextCasted = (FluxRestModelContext<TestModel, int>)modelContext;
        var serializerOptions = modelContextCasted.ServiceOptions.SerializerOptions;

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
            .AddModel<TestModel>("test-model");
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
                .AddModel<TestModel>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var fluxContext = serviceProvider.GetRequiredService<IFluxContext>();

        var service = fluxContext.Service("service");
        Assert.NotNull(service);
        Assert.True(service is FluxServiceContext);

        var provider = ((FluxServiceContext)service).Provider;
        Assert.True(provider is FluxRestServiceFactory);

        var modelContextFromService = service.Model<TestModel>();
        Assert.NotNull(modelContextFromService);
        Assert.True(modelContextFromService is FluxRestModelContext<TestModel>);

        var modelContextFromFluxContext = fluxContext.Model<TestModel>();
        Assert.NotNull(modelContextFromFluxContext);
        Assert.True(modelContextFromFluxContext is FluxRestModelContext<TestModel>);
    }

    [Fact]
    public void AddFlux2Services_GetServiceContextsFromDiContainer_Returns()
    {
        var services = new ServiceCollection();
        services.AddFlux(flux =>
        {
            flux.AddService("service1")
            .UsingRest("http://localhost1")
                .AddModel<TestModel>("test");

            flux.AddService("service2")
            .UsingRest("http://localhost2")
                .AddModel<TestModel>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var serviceContexts = serviceProvider.GetRequiredService<IEnumerable<IFluxServiceContext>>();
        Assert.NotNull(serviceContexts);
        Assert.True(serviceContexts.Any());
        Assert.True(serviceContexts.Count() == 2);
    }
}