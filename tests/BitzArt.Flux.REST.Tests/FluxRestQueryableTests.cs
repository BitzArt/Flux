using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System.Net.Http.Json;
using System.Net;

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
    public async Task FirstOrDefaultAsync_OnRestSetWithNoTKeyWithNoExpression_ThrowsNotSupported()
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

        await Assert.ThrowsAsync<NotSupportedException>(() => set.FirstOrDefaultAsync());
    }

    [Fact]
    public async Task FirstOrDefaultAsync_OnRestSetWithTKeyWithNoExpression_ThrowsNotSupported()
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

        await Assert.ThrowsAsync<NotSupportedException>(() => set.FirstOrDefaultAsync());
    }

    [Fact]
    public async Task FirstOrDefaultAsync_OnQuery_CallsEndpointById()
    {
        var baseUrl = "https://mocked.service";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/1")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(TestModel.GetAll(10).FirstOrDefault(x => x.Id == 1)));
        });

        var result = await setContext.Where(x => x.Id == 1).FirstOrDefaultAsync();

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
}
