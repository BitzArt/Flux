using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

file class TestModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
}

public class ServiceRegistrationTests
{
    [Fact]
    public async Task Get_TestItem_Returns()
    {
        var services = new ServiceCollection();
        const string serviceName = "service1";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson("Data", json =>
                {
                    json.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    json.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    json.Converters.Add(new JsonStringEnumConverter());
                    json.WriteIndented = true;
                })
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id);
        });

        var serviceProvider = services.BuildServiceProvider();

        var set = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();
        var item = await set.GetAsync(1);

        Assert.NotNull(item);
        Assert.Equal(1, item.Id);
    }
    
    [Fact]
    public void UsingJson_WithModel_AddsFactoryAndSetContext()
    {
        var services = new ServiceCollection();
        const string serviceName = "service1";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson("Data", json =>
                {
                    json.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    json.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    json.Converters.Add(new JsonStringEnumConverter());
                    json.WriteIndented = true;
                })
                .AddSet<TestModel>("test-model.set.json").WithKey(x => x.Id);
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
                .UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id);
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
        
        services.AddFlux(flux =>
        {
            flux.AddService("service1")
                .UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id);
            
            flux.AddService("service2")
                .UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json").WithKey(x => x.Id);
        });

        var serviceProvider = services.BuildServiceProvider();
        var serviceContexts = serviceProvider.GetRequiredService<IEnumerable<IFluxServiceContext>>().ToList();
        
        Assert.NotNull(serviceContexts);
        Assert.True(serviceContexts.Any());
        Assert.True(serviceContexts.Count == 2);
    }
}