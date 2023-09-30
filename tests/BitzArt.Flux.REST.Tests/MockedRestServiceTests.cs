using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http.Json;

namespace BitzArt.Flux;

public class MockedRestServiceTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public async Task GetAllAsync_MockedHttpClient_ReturnsAll(int modelCount)
    {
        var baseUrl = "https://mocked.service";

        var modelContext = TestModelContext.GetTestModelContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(TestModel.GetAll(modelCount)));
        });

        var result = await modelContext.GetAllAsync();

        Assert.NotNull(result);
        if (modelCount > 0) Assert.True(result.Any());
        Assert.True(result.Count() == modelCount);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 0, 10)]
    [InlineData(100, 0, 10)]
    [InlineData(1000, 0, 10)]
    [InlineData(10, 5, 10)]
    [InlineData(100, 1, 100)]
    public async Task GetPageAsync_MockedHttpClient_ReturnsPage(int modelCount, int offset, int limit)
    {
        var baseUrl = "https://mocked.service";

        var modelContext = TestModelContext.GetTestModelContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model?offset={offset}&limit={limit}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(TestModel.GetPage(modelCount, offset, limit)));
        });

        var result = await modelContext.GetPageAsync(offset, limit);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        if (offset < modelCount) Assert.True(result.Data.Any());
        if (offset + limit > modelCount)
        {
            var shouldCount = modelCount - offset;
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
    public async Task GetAsync_MockedHttpClient_ReturnsModel(string baseUrl)
    {
        var modelCount = 10;
        var id = 1;

        var modelContext = TestModelContext.GetTestModelContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(
                TestModel.GetAll(modelCount).FirstOrDefault(x => x.Id == id)));
        });

        var result = await modelContext.GetAsync(id);

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
    public async Task GetAsync_CustomIdEndpointLogic_ReturnsModel(string baseUrl)
    {
        var modelCount = 1;

        var modelContext = TestModelContext.GetTestModelContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/specific")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(
                TestModel.GetAll(modelCount).FirstOrDefault(x => x.Id == 1)));
        });

        ((FluxRestModelContext<TestModel>)modelContext)
            .ModelOptions.GetIdEndpointAction = (key, parameters) => "model/specific";

        var result = await modelContext.GetAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetAsyncWithParameters_MockedHttpClient_Returns()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;

        var database = TestModel.GetAll(100);
        var changedName = "Changed Name";
        var defaultModel = database.First(x => x.Id == id);
        var defaultName = defaultModel.Name;
        var changedModel = new TestModel() { Id = id, Name = changedName };

        var modelContext = (FluxRestModelContext<TestModel, int>)
            TestModelContext.GetTestModelContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/{id}?changeName=False")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(defaultModel));

            x.When($"{baseUrl.TrimEnd('/')}/model/{id}?changeName=True")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(changedModel));
        });

        modelContext.ModelOptions.GetIdEndpointAction = (id, parameters) =>
        {
            return $"model/{id}?changeName={parameters!.First()}";
        };

        var resultWithParameterFalse = await modelContext.GetAsync(id, false);
        Assert.Equal(defaultName, resultWithParameterFalse.Name);

        var resultWithParameterTrue = await modelContext.GetAsync(id, true);
        Assert.Equal(changedName, resultWithParameterTrue.Name);
    }

    [Fact]
    public async Task GetPageAsyncWithParameters_MockedHttpClient_Returns()
    {
        var baseUrl = "https://mocked.service";

        var modelContext = (FluxRestModelContext<TestModel, int>)
            TestModelContext.GetTestModelContext(baseUrl, x =>
            {
                x.When($"{baseUrl.TrimEnd('/')}/test-1?offset=0&limit=10")
                .Respond(HttpStatusCode.OK,
                JsonContent.Create(TestModel.GetPage(100, 0, 10)));
            });

        modelContext.ModelOptions.Endpoint = "test-{number}";

        var result = await modelContext.GetPageAsync(0, 10, 1);
        Assert.NotNull(result);
        Assert.True(result.Data!.Count() == 10);
    }

    [Fact]
    public async Task GetPageAsyncWithPageEndpoint_MockedHttpClient_Returns()
    {
        var baseUrl = "https://mocked.service";

        var modelContext = (FluxRestModelContext<TestModel, int>)
            TestModelContext.GetTestModelContext(baseUrl, x =>
            {
                x.When($"{baseUrl.TrimEnd('/')}/1/models?offset=0&limit=10")
                .Respond(HttpStatusCode.OK,
                JsonContent.Create(TestModel.GetPage(100, 0, 10)));
            });

        modelContext.ModelOptions.PageEndpoint = "{parentId}/models";

        var result = await modelContext.GetPageAsync(0, 10, 1);
        Assert.NotNull(result);
        Assert.True(result.Data!.Count() == 10);
    }
}