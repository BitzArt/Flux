using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.Json;

public class EnrichQueryExtensionTests
{
    [Fact]
    public async Task EnrichQuery_GetAll_ShouldFilterResults()
    {
        // Arrange
        const string jsonData =
            """
            [
                { "id": 1 },
                { "id": 2 }
            ]
            """;

        var services = new ServiceCollection();
        var enrichQueryCalled = false;

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .AddSet<TestModel>()
                    .FromJson(jsonData)
                    .EnrichQuery((query, _) =>
                    {
                        enrichQueryCalled = true;
                        return query.Where(x => x.Id > 1);
                    });
        });

        var serviceProvider = services.BuildServiceProvider();
        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        // Act
        var result = await setContext.GetAllAsync();

        // Assert
        Assert.True(enrichQueryCalled);
        var item = Assert.Single(result);
        Assert.Equal(2, item.Id);
    }

    [Fact]
    public async Task EnrichQuery_GetPage_ShouldFilterBeforePagination()
    {
        // Arrange
        const string jsonData =
            """
            [
                { "id": 1 },
                { "id": 2 }
            ]
            """;

        var services = new ServiceCollection();
        var enrichQueryCalled = false;

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .AddSet<TestModel>()
                    .FromJson(jsonData)
                    .EnrichQuery((query, _) =>
                    {
                        enrichQueryCalled = true;
                        return query.Where(x => x.Id > 1);
                    });
        });

        var serviceProvider = services.BuildServiceProvider();
        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        // Act
        var result = await setContext.GetPageAsync(offset: 0, limit: 10);

        // Assert
        Assert.True(enrichQueryCalled);
        Assert.NotNull(result.Items);
        var item = Assert.Single(result.Items);
        Assert.Equal(2, item.Id);
    }

    [Fact]
    public async Task EnrichQuery_Get_ShouldFilterByDescriptorId()
    {
        // Arrange
        const string jsonData =
            """
            [
                { "id": 1 },
                { "id": 2 }
            ]
            """;

        var services = new ServiceCollection();
        var enrichQueryCalled = false;

        services.AddFlux(flux =>
        {
            flux.AddService("service 1")
                .UsingJson()
                    .AddSet<TestModel>()
                    .FromJson(jsonData)
                    .EnrichQuery((query, descriptor) =>
                    {
                        enrichQueryCalled = true;

                        return descriptor is GetOperationDescriptor getOperation
                            ? query.Where(x => x.Id == (int)getOperation.Id!)
                            : query;
                    });
        });

        var serviceProvider = services.BuildServiceProvider();
        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel>>();

        // Act
        var result = await setContext.GetAsync(2);

        // Assert
        Assert.True(enrichQueryCalled);
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
    }

    [Fact]
    public void EnrichQuery_WhenAlreadyConfigured_ShouldThrow()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act/Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            services.AddFlux(flux =>
            {
                flux.AddService("service 1")
                    .UsingJson()
                        .AddSet<TestModel>()
                        .EnrichQuery((query, _) => query)
                        .EnrichQuery((query, _) => query);
            });
        });
    }
}
