using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class FluxRestQueryableTests
{
    [Fact]
    public void Where_OnRestSetWithNoTKey_CreatesFluxRestQueryable()
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
    public void Where_OnRestSetWithTKey_CreatesFluxRestQueryable()
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

        var query = set.Where(x =>true);

        Assert.IsType<FluxRestQueryable<TestModel, int>>(query);
    }
}
