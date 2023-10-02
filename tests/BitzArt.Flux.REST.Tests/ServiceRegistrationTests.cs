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
    public void UsingRest_WithModel_AddsFactoryAndSetContext()
    {
        var services = new ServiceCollection();

        var serviceName = "SomeExternalService";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
            .UsingRest("http://localhost")
            .AddSet<TestModel, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IFluxFactory>();
        Assert.NotNull(factory);
        Assert.True(factory.ServiceContexts.Count > 0);

        Assert.True(factory.ServiceContexts.Count == 1);
        var provider = factory.ServiceContexts.Single();

        Assert.Equal(serviceName, provider.ServiceName);

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel, int>>();
        Assert.NotNull(setContext);
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
            .AddSet<TestModel, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();
        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel, int>>();
        Assert.NotNull(setContext);

        var setContextCasted = (FluxRestSetContext<TestModel, int>)setContext;

        Assert.Contains(setContextCasted.HttpClient.DefaultRequestHeaders,
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
            .AddSet<TestModel, int>("test");
        });

        Assert.True(configUsed);

        var serviceProvider = services.BuildServiceProvider();
        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel, int>>();
        Assert.NotNull(setContext);

        var setContextCasted = (FluxRestSetContext<TestModel, int>)setContext;
        var serializerOptions = setContextCasted.ServiceOptions.SerializerOptions;

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
            .AddSet<TestModel>("test-model");
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
                .AddSet<TestModel>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var fluxContext = serviceProvider.GetRequiredService<IFluxContext>();

        var service = fluxContext.Service("service");
        Assert.NotNull(service);
        Assert.True(service is FluxServiceContext);

        var provider = ((FluxServiceContext)service).Provider;
        Assert.True(provider is FluxRestServiceFactory);

        var setContextFromService = service.Set<TestModel>();
        Assert.NotNull(setContextFromService);
        Assert.True(setContextFromService is FluxRestSetContext<TestModel>);

        var setContextFromFluxContext = fluxContext.Set<TestModel>();
        Assert.NotNull(setContextFromFluxContext);
        Assert.True(setContextFromFluxContext is FluxRestSetContext<TestModel>);
    }

    [Fact]
    public void AddFlux2Services_GetServiceContextsFromDiContainer_Returns()
    {
        var services = new ServiceCollection();
        services.AddFlux(flux =>
        {
            flux.AddService("service1")
            .UsingRest("http://localhost1")
                .AddSet<TestModel>("test");

            flux.AddService("service2")
            .UsingRest("http://localhost2")
                .AddSet<TestModel>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var serviceContexts = serviceProvider.GetRequiredService<IEnumerable<IFluxServiceContext>>();
        Assert.NotNull(serviceContexts);
        Assert.True(serviceContexts.Any());
        Assert.True(serviceContexts.Count() == 2);
    }

    [Fact]
    public void AddSet_SameModelDifferentNames_Configures()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service1")
            .UsingRest()
            .AddSet<TestModel>("endpoint", "test-set-1")
            .AddSet<TestModel>("endpoint", "test-set-2");
        });

        var serviceProvider = services.BuildServiceProvider();
        var flux = serviceProvider.GetRequiredService<IFluxContext>();
        var service = flux.Service("service1");

        var set1FromService = service.Set<TestModel>("test-set-1");
        Assert.NotNull(set1FromService);

        var set2FromService = service.Set<TestModel>("test-set-2");
        Assert.NotNull(set2FromService);

        Assert.Throws<SetConfigurationNotFoundException>(() =>
        {
            var setNoNameFromService = service.Set<TestModel>();
        });

        var set1FromFlux = flux.Set<TestModel>("service1", "test-set-1");
        Assert.NotNull(set1FromFlux);

        var set2FromFlux = flux.Set<TestModel>("service1", "test-set-2");
        Assert.NotNull(set2FromFlux);

        Assert.Throws<SetConfigurationNotFoundException>(() =>
        {
            var setNoNameFromFlux = flux.Set<TestModel>("service1");
        });

        Assert.Throws<FluxServiceProviderNotFoundException>(() =>
        {
            var setNoNameFromFlux = flux.Set<TestModel>();
        });
    }

    [Fact]
    public void AddSet_SameModelTwiceNoName_Throws()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            var builder = flux.AddService("service1").UsingRest();

            builder.AddSet<TestModel>();

            Assert.Throws<SetAlreadyRegisteredException>(() =>
            {
                builder.AddSet<TestModel>();
            });
        });
    }
}