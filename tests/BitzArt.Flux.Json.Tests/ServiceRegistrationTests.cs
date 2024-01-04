using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

file class TestModel
{
    public int Id { get; set; }
}

public class ServiceRegistrationTests
{
    [Fact]
    public void UsingJson_WithModel_AddsFactoryAndSetContext()
    {
        var services = new ServiceCollection();

        const string serviceName = "SomeExternalService";

        services.AddFlux(flux =>
        {
            flux.AddService(serviceName)
                .UsingJson("Data")
                .AddSet<TestModel, int>("test-model.set.json");
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
}