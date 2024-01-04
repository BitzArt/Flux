using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class ServiceRegistrationTests
{
    [Fact]
    public void UsingJson_WithModel_AddsFactoryAndSetContext()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        
        const string serviceName = "service1";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson("Data")
                .AddSet<TestModel>("test-model.set.json").WithKey(x => x.Id!);
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
        services.AddLogging();
        
        const string serviceName = "service1";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id!);
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
        Assert.True(setContextFromService is FluxJsonSetContext<TestModel>);

        var setContextFromFluxContext = fluxContext.Set<TestModel>();
        Assert.NotNull(setContextFromFluxContext);
        Assert.True(setContextFromFluxContext is FluxJsonSetContext<TestModel>);
    }
    
    [Fact]
    public void AddFlux2Services_GetServiceContextsFromDiContainer_Returns()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        
        services.AddFlux(flux =>
        {
            flux.AddService("service1")
                .UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id!);
            
            flux.AddService("service2")
                .UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id!);
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
        services.AddLogging();
        
        services.AddFlux(flux =>
        {
            flux.AddService("service1")
                .UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json", "test-set-1").WithKey(x => x.Id!)
                .AddSet<TestModel, int>("test-model.set.json", "test-set-2").WithKey(x => x.Id!);
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
        services.AddLogging();
        
        services.AddFlux(flux =>
        {
            var builder = flux.AddService("service1").UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json");

            Assert.Throws<SetAlreadyRegisteredException>(() =>
            {
                builder.AddSet<TestModel, int>("test-model.set.json");
            });
        });
    }
    
    [Fact]
    public void UsingJson_WithJsonConfiguration_Configures()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        
        const string serviceName = "service1";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson("Data", json =>
                {
                    json.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    json.Converters.Add(new JsonStringEnumConverter());
                    json.WriteIndented = true;
                })
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id!.Value);
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
}