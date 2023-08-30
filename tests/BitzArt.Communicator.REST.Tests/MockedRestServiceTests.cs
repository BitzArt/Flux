using BitzArt.Pagination;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.Extensions.Logging;
using RichardSzalay.MockHttp;
using System.Net.Http.Json;
using System.Net;

namespace BitzArt.Communicator;

public class MockedRestServiceTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task GetAllAsync_MockedHttpClient_ReturnsAll(int entityCount)
    {
        var entityContext = TestEntityContext.GetTestEntityContext(entityCount, x =>
        {
            x.When($"{MockedService.BaseUrl}/entity")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(TestEntity.GetAll(entityCount)));
        });

        var result = await entityContext.GetAllAsync();

        Assert.NotNull(result);
        if (entityCount > 0) Assert.True(result.Any());
        Assert.True(result.Count() == entityCount);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 0, 10)]
    [InlineData(100, 0, 10)]
    [InlineData(1000, 0, 10)]
    [InlineData(10, 5, 10)]
    [InlineData(100, 1, 100)]
    public async Task GetPageAsync_MockedHttpClient_ReturnsPage(int entityCount, int offset, int limit)
    {
        var entityContext = TestEntityContext.GetTestEntityContext(entityCount, x =>
        {
            x.When($"{MockedService.BaseUrl}/entity?offset={offset}&limit={limit}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(TestEntity.GetPage(entityCount, offset, limit)));
        });

        var result = await entityContext.GetPageAsync(offset, limit);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        if (offset < entityCount) Assert.True(result.Data.Any());
        if (offset + limit > entityCount)
        {
            var shouldCount = entityCount - offset;
            Assert.Equal(shouldCount, result.Data.Count());
        }
    }

    [Fact]
    public async Task GetAsync_MockedHttpClient_ReturnsEntity()
    {
        var entityCount = 10;
        var id = 1;

        var entityContext = TestEntityContext.GetTestEntityContext(entityCount, x =>
        {
            x.When($"{MockedService.BaseUrl}/entity/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(
                TestEntity.GetAll(entityCount).FirstOrDefault(x => x.Id == id)));
        });

        var result = await entityContext.GetAsync(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task GetAsync_CustomIdEndpointLogic_ReturnsEntity()
    {
        var entityCount = 1;

        var entityContext = TestEntityContext.GetTestEntityContext(entityCount, x =>
        {
            x.When($"{MockedService.BaseUrl}/entity/specific")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(
                TestEntity.GetAll(entityCount).FirstOrDefault(x => x.Id == 1)));
        });

        ((CommunicatorRestEntityContext<TestEntity>)entityContext)
            .EntityOptions.GetIdEndpointAction = (key) => "entity/specific";

        var result = await entityContext.GetAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
}