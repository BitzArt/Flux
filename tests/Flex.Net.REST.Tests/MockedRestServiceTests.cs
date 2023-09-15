using RichardSzalay.MockHttp;
using System.Net.Http.Json;
using System.Net;

namespace Flex;

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
        var baseUrl = "https://mocked.service";

        var entityContext = TestEntityContext.GetTestEntityContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/entity")
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
        var baseUrl = "https://mocked.service";

        var entityContext = TestEntityContext.GetTestEntityContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/entity?offset={offset}&limit={limit}")
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

    [Theory]
    [InlineData("http://mockedservice")]
    [InlineData("http://mocked.service/")]
    [InlineData("https://mockedservice")]
    [InlineData("https://mockedservice/")]
    [InlineData("https://mocked.service")]
    [InlineData("https://mockedservice/test/")]
    [InlineData("https://mockedservice/test")]
    [InlineData("https://mocked.service/second.segment/third.segment/test.test/test/")]
    public async Task GetAsync_MockedHttpClient_ReturnsEntity(string baseUrl)
    {
        var entityCount = 10;
        var id = 1;

        var entityContext = TestEntityContext.GetTestEntityContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/entity/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(
                TestEntity.GetAll(entityCount).FirstOrDefault(x => x.Id == id)));
        });

        var result = await entityContext.GetAsync(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
    }

    [Theory]
    [InlineData("http://mockedservice")]
    [InlineData("http://mocked.service/")]
    [InlineData("https://mockedservice")]
    [InlineData("https://mockedservice/")]
    [InlineData("https://mocked.service")]
    [InlineData("https://mockedservice/test/")]
    [InlineData("https://mockedservice/test")]
    [InlineData("https://mocked.service/second.segment/third.segment/test.test/test/")]
    public async Task GetAsync_CustomIdEndpointLogic_ReturnsEntity(string baseUrl)
    {
        var entityCount = 1;

        var entityContext = TestEntityContext.GetTestEntityContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/entity/specific")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(
                TestEntity.GetAll(entityCount).FirstOrDefault(x => x.Id == 1)));
        });

        ((CommunicatorRestEntityContext<TestEntity>)entityContext)
            .EntityOptions.GetIdEndpointAction = (key, parameters) => "entity/specific";

        var result = await entityContext.GetAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsyncWithParameters_MockedHttpClient_Returns()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;

        var database = TestEntity.GetAll(100);
        var changedName = "Changed Name";
        var defaultEntity = database.First(x => x.Id == id);
        var defaultName = defaultEntity.Name;
        var changedEntity = new TestEntity() { Id = id, Name = changedName };

        var entityContext = (CommunicatorRestEntityContext<TestEntity, int>)
            TestEntityContext.GetTestEntityContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/entity/{id}?changeName=False")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(defaultEntity));

            x.When($"{baseUrl.TrimEnd('/')}/entity/{id}?changeName=True")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(changedEntity));
        });

        entityContext.EntityOptions.GetIdEndpointAction = (id, parameters) =>
        {
            return $"entity/{id}?changeName={parameters!.First()}";
        };

        var resultWithParameterFalse = await entityContext.GetAsync(id, false);
        Assert.Equal(defaultName, resultWithParameterFalse.Name);

        var resultWithParameterTrue = await entityContext.GetAsync(id, true);
        Assert.Equal(changedName, resultWithParameterTrue.Name);
    }

    [Fact]
    public async Task GetPageAsyncWithParameters_MockedHttpClient_Returns()
    {
        var baseUrl = "https://mocked.service";

        var entityContext = (CommunicatorRestEntityContext<TestEntity, int>)
            TestEntityContext.GetTestEntityContext(baseUrl, x =>
            {
                x.When($"{baseUrl.TrimEnd('/')}/test-1?offset=0&limit=10")
                .Respond(HttpStatusCode.OK,
                JsonContent.Create(TestEntity.GetPage(100, 0, 10)));
            });

        entityContext.EntityOptions.Endpoint = "test-{number}";

        var result = await entityContext.GetPageAsync(0, 10, 1);
        Assert.NotNull(result);
        Assert.True(result.Data!.Count() == 10);
    }
}