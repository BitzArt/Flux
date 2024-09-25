using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class ServiceRegistrationTests
{
    [Fact]
    public void UsingJson_WithModel_AddsFactoryAndSetContext()
    {
        var services = new ServiceCollection();
        
        const string serviceName = "service1";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .AddSet<TestModel>()
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!);
        });

        var serviceProvider = services.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IFluxFactory>();
        Assert.NotNull(factory);
        Assert.True(factory.ServiceContexts.Count > 0);

        Assert.True(factory.ServiceContexts.Count == 1);
        var provider = factory.ServiceContexts.Single();

        Assert.Equal(serviceName, provider.ServiceName);

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();
        Assert.NotNull(setContext);
    }
    
    [Fact]
    public void AddFlux_GetAllPackageSignatureElementsFromFluxContext_ReturnsAll()
    {
        var services = new ServiceCollection();
        
        const string serviceName = "service1";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .AddSet<TestModel, int>()
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();
        var fluxContext = serviceProvider.GetRequiredService<IFluxContext>();

        var service = fluxContext.Service(serviceName);
        Assert.NotNull(service);
        Assert.True(service is FluxServiceContext);

        var provider = ((FluxServiceContext)service).Provider;
        Assert.True(provider is FluxJsonServiceFactory);

        var setContextFromService = service.Set<TestModel>();
        Assert.NotNull(setContextFromService);
        Assert.True(setContextFromService is FluxJsonSetContext<TestModel, object>);

        var setContextFromFluxContext = fluxContext.Set<TestModel>();
        Assert.NotNull(setContextFromFluxContext);
        Assert.True(setContextFromFluxContext is FluxJsonSetContext<TestModel, object>);
    }
    
    [Fact]
    public void AddFlux2Services_GetServiceContextsFromDiContainer_Returns()
    {
        var services = new ServiceCollection();
        
        services.AddFlux(flux =>
        {
            flux.AddService("service1")
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .AddSet<TestModel, int>()
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!.Value);

            flux.AddService("service2")
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .AddSet<TestModel, int>()
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();
        var serviceContexts = serviceProvider.GetRequiredService<IEnumerable<IFluxServiceContext>>().ToList();
        
        Assert.NotNull(serviceContexts);
        Assert.True(serviceContexts.Any());
        Assert.True(serviceContexts.Count == 2);
    }
    
    [Fact]
    public void AddSet_SameModelDifferentNames_Configures()
    {
        var services = new ServiceCollection();
        
        services.AddFlux(flux =>
        {
            flux.AddService("service1")
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .AddSet<TestModel, int>("test-set-1")
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!.Value)
                    .AddSet<TestModel, int>("test-set-2")
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!.Value);
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
            _ = service.Set<TestModel>();
        });

        var set1FromFlux = flux.Set<TestModel>("service1", "test-set-1");
        Assert.NotNull(set1FromFlux);

        var set2FromFlux = flux.Set<TestModel>("service1", "test-set-2");
        Assert.NotNull(set2FromFlux);

        Assert.Throws<SetConfigurationNotFoundException>(() =>
        {
            _ = flux.Set<TestModel>("service1");
        });

        Assert.Throws<FluxServiceProviderNotFoundException>(() =>
        {
            _ = flux.Set<TestModel>();
        });
    }
    
    [Fact]
    public void AddSet_SameModelTwiceNoName_Throws()
    {
        var services = new ServiceCollection();
        
        services.AddFlux(flux =>
        {
            var builder = flux.AddService("service1")
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .AddSet<TestModel, int>()
                        .FromJsonFile("test-model.set.json");

            Assert.Throws<SetAlreadyRegisteredException>(() =>
            {
                builder.AddSet<TestModel, int>();
            });
        });
    }
    
    [Fact]
    public void UsingJson_WithJsonConfiguration_Configures()
    {
        var services = new ServiceCollection();
        
        const string serviceName = "service1";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson()
                    .WithBaseFilePath("Data")
                    .ConfigureJson(json =>
                    {
                        json.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                        json.Converters.Add(new JsonStringEnumConverter());
                        json.WriteIndented = true;
                    })
                    .AddSet<TestModel, int>()
                        .FromJsonFile("test-model.set.json")
                        .WithKey(x => x.Id!.Value);
        });

        var serviceProvider = services.BuildServiceProvider();

        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();
        var setContextCasted = (FluxJsonSetContext<TestModel, int>)setContext;
        var serializerOptions = setContextCasted.ServiceOptions.SerializerOptions;
        
        Assert.NotNull(serializerOptions);
        Assert.Equal(JsonIgnoreCondition.WhenWritingNull, serializerOptions.DefaultIgnoreCondition);
        Assert.True(serializerOptions.WriteIndented);
        Assert.Contains(serializerOptions.Converters, x => x.GetType() == typeof(JsonStringEnumConverter));
    }

    [Fact]
    public void AddSet_TwoSetsSameModelDifferentNames_AddsNamed()
    {
        var services = new ServiceCollection();

        services.AddFlux(flux =>
        {
            flux.AddService("service1")
            .UsingJson()
                .WithBaseFilePath("Data")
                .AddSet<TestModel>("test-set-1")
                    .FromJsonFile("test-model.set.json")
                .AddSet<TestModel>("test-set-2")
                    .FromJsonFile("test-model.set.json");
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
}