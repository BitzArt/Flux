using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class FluxRestQueryableTests
{
    [Fact]
    public void Where_OnRestSetWithNoTKey_CreatesFluxRestQueryableOfCorrectType()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddFlux(x =>
        {
            x.AddService("test-service")
                .UsingRest()
                    .AddSet<TestModel>();
        });
        var services = serviceCollection.BuildServiceProvider();
        var set = services.GetRequiredService<IFluxSetContext<TestModel>>();

        var query = set.Where(x => true);

        Assert.IsType<FluxRestQueryable<TestModel>>(query);
    }

    [Fact]
    public void Where_OnRestSetWithTKey_CreatesFluxRestQueryableOfCorrectType()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddFlux(x =>
        {
            x.AddService("test-service")
                .UsingRest()
                    .AddSet<TestModel, int>();
        });
        var services = serviceCollection.BuildServiceProvider();
        var set = services.GetRequiredService<IFluxSetContext<TestModel>>();

        var query = set.Where(x => true);

        Assert.IsType<FluxRestQueryable<TestModel, int>>(query);
    }

    [Fact]
    public void FirstOrDefaultAsync_OnRestSetWithNoTKeyWithNoExpression_ThrowsNotSupported()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddFlux(x =>
        {
            x.AddService("test-service")
                .UsingRest()
                    .AddSet<TestModel>();
        });
        var services = serviceCollection.BuildServiceProvider();
        var set = services.GetRequiredService<IFluxSetContext<TestModel>>();

        Assert.ThrowsAsync<NotSupportedException>(() => set.FirstOrDefaultAsync());
    }

    [Fact]
    public void FirstOrDefaultAsync_OnRestSetWithTKeyWithNoExpression_ThrowsNotSupported()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddFlux(x =>
        {
            x.AddService("test-service")
                .UsingRest()
                    .AddSet<TestModel, int>();
        });
        var services = serviceCollection.BuildServiceProvider();
        var set = services.GetRequiredService<IFluxSetContext<TestModel>>();

        Assert.ThrowsAsync<NotSupportedException>(() => set.FirstOrDefaultAsync());
    }
}
