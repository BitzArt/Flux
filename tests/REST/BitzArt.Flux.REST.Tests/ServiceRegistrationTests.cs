using BitzArt.Flux.REST;
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
    public void AddFlux_Empty_ShouldAddFluxFactory()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux => { });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IFluxFactory>();
        Assert.NotNull(factory);
        Assert.Empty(factory.ServiceContexts);
    }

    [Fact]
    public void UsingRest_OnServicePrebuilder_ShouldAddAndConfigureRestServiceContext()
    {
        var services = new ServiceCollection();

        // Doesn't matter, but every service requires a name
        var serviceName = "SomeExternalService";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingRest("http://localhost");
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IFluxFactory>();

        Assert.Single(factory.ServiceContexts);
        var provider = factory.ServiceContexts.Single();

        Assert.Equal(serviceName, provider.ServiceName);

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel, int>>();
        Assert.NotNull(setContext);

        Assert.IsType<FluxRestSetContext<TestModel, int>>(setContext);
    }

    [Fact]
    public void AddSet_Basic_ShouldAddAndConfigureSetContext()
    {
        var services = new ServiceCollection();

        // Doesn't matter, but every service requires a name
        var serviceName = "SomeExternalService";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingRest("http://localhost")
                    .AddSet<TestModel, int>("test");
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IFluxFactory>();
        var provider = factory.ServiceContexts.Single();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel, int>>();
        Assert.NotNull(setContext);

        Assert.IsType<FluxRestSetContext<TestModel, int>>(setContext);
    }

    [Fact]
    public void ConfigureHttpClient_WithAction_ShouldPerformActionAndConfigureHttpClient()
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
        var httpClient = setContextCasted.HttpClient;

        Assert.Contains(httpClient.DefaultRequestHeaders,
            x => x.Key == testHeader.Key && x.Value.Single() == testHeader.Value);
    }

    [Fact]
    public void ConfigureJson_WithAction_ShouldPerformActionAndConfigureJsonSerializer()
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
    public void ResolveAllDescendantsOfFluxContext_AfterAddFlux_ShouldResolveAll()
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

        var setContextFromService = service.Set<TestModel>();
        Assert.NotNull(setContextFromService);
        Assert.True(setContextFromService is FluxRestSetContext<TestModel, object>);

        var setContextFromFluxContext = fluxContext.Set<TestModel>();
        Assert.NotNull(setContextFromFluxContext);
        Assert.True(setContextFromFluxContext is FluxRestSetContext<TestModel, object>);
    }

    [Fact]
    public void ResolveServiceContextsFromDiContainer_AfterAddFluxWith2Services_ShouldResolve()
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
        Assert.Equal(2, serviceContexts.Count());
    }

    [Fact]
    public void AddSet_WithTwoSetsHavingSameModelAndDifferentNames_ShouldAddAndConfigureSets()
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

        Assert.ThrowsAny<FluxException>(() =>
        {
            var setNoNameFromService = service.Set<TestModel>();
        });

        var set1FromFlux = flux.Set<TestModel>("service1", "test-set-1");
        Assert.NotNull(set1FromFlux);

        var set2FromFlux = flux.Set<TestModel>("service1", "test-set-2");
        Assert.NotNull(set2FromFlux);

        Assert.ThrowsAny<FluxException>(() =>
        {
            var setNoNameFromFlux = flux.Set<TestModel>("service1");
        });

        Assert.ThrowsAny<FluxException>(() =>
        {
            var setNoNameFromFlux = flux.Set<TestModel>();
        });
    }

    [Fact]
    public void AddSet_WithSameModelTwiceButNoName_ShouldThrow()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            var builder = flux.AddService("service1").UsingRest();

            builder.AddSet<TestModel>();

            Assert.ThrowsAny<FluxException>(() =>
            {
                builder.AddSet<TestModel>();
            });
        });
    }

    public void TestMethod()
    {
        var setContext = new FluxRestSetContext<TestModel, int>(null!, null!, null!, null!);

        var parameters = new Dictionary<string, object>()
        {
            { "key", "value" }
        };

        _ = setContext.GetAsync(1, [new("1", 1)], CancellationToken.None);
    }
}