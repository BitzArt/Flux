using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor;
using System.Text.Json.Serialization;

namespace BitzArt.Flux.MudBlazor.Tests;

public class FluxSetDataProviderTests
{
    [Fact]
    public async Task OnQuery_WhenRequestCompleted_ShouldTrigger()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddFlux(flux =>
        {
            flux.AddService("test-service")
                .UsingJson()
                    .AddSet<TestModel>()
                        .FromJson(TestDatabase);
        });

        serviceCollection.AddFluxSetDataProvider<TestModel>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var dataProvider = serviceProvider.GetRequiredService<IFluxSetDataProvider<TestModel>>();

        var tableState = new TableState()
        {
            Page = 0,
            PageSize = 10
        };

        var triggered = false;
        FluxSetDataPageQuery<TestModel>? query = null;

        dataProvider.OnResult += args =>
        {
            triggered = true;
            query = args.Query;
        };

        // Act
        await dataProvider.Data.Invoke(tableState, default);

        // Assert
        Assert.True(triggered);
        Assert.NotNull(query);

        Assert.Equal(tableState, query!.TableState);

        Assert.NotNull(query.Parameters);
        Assert.Empty(query.Parameters);

        Assert.NotNull(query.Result);
        Assert.Equal(TestModelCount, query.Result.TotalItems);
        Assert.Contains(query.Result.Items!, item => item.Id == 1);
        Assert.Contains(query.Result.Items!, item => item.Id == 2);
        Assert.Contains(query.Result.Items!, item => item.Id == 3);
    }

    [Fact]
    public async Task RestoreLastQuery_WithQuery_ShouldRestore()
    {
        var loggerFactory = new LoggerFactory();

        var dataProvider = new FluxSetDataProvider<TestModel>(loggerFactory);
        var tableState = new TableState()
        {
            Page = 0,
            PageSize = 10
        };
        var query = new FluxSetDataPageQuery<TestModel>()
        {
            TableState = tableState,
            Parameters = [],
            Result = new TableData<TestModel>()
            {
                Items =
                [
                    new() { Id = 1, Name = "Test model 1" },
                    new() { Id = 2, Name = "Test model 2" },
                    new() { Id = 3, Name = "Test model 3" }
                ],
                TotalItems = 3
            }
        };

        var triggered = false;
        dataProvider.OnResult += q =>
        {
            triggered = true;
        };

        // Act
        dataProvider.RestoreLastQuery(query);

        // Assert
        Assert.False(triggered);

        Assert.Equal(query, dataProvider.LastQuery);

        await dataProvider.Data.Invoke(tableState, default);

        // Should restore from LastQuery and not trigger OnResult
        Assert.False(triggered);

        Assert.Equal(query, dataProvider.LastQuery);
    }

    private class TestModel
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }

    private const int TestModelCount = 3;
    private const string TestDatabase =
        """
        [
            {
                "id": 1,
                "name": "Test model 1"
            },
            {
                "id": 2,
                "name": "Test model 2"
            },
            {
                "id": 3,
                "name": "Test model 3"
            }
        ]
        """;
}