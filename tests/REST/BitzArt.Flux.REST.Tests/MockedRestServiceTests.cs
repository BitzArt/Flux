using BitzArt.Flux.REST;
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
    public async Task GetAllAsync_MockedHttpClient_ReturnsAll(int setCount)
    {
        var baseUrl = "https://mocked.service";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(TestModel.GetAll(setCount)));
        });

        var result = await setContext.GetAllAsync();

        Assert.NotNull(result);
        if (setCount > 0) Assert.True(result.Any());
        Assert.True(result.Count() == setCount);
    }

    [Fact]
    public async Task GetAllAsync_WithQuery_ReturnsAll()
    {
        var baseUrl = "https://mocked.service";
        var query = "?sort=id";
        var setCount = 10;

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model{query}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(TestModel.GetAll(setCount)));
        });

        ((FluxRestSetContext<TestModel, int>)setContext)
          .SetOptions.EndpointOptions.Path = "model{query}";

        var result = await setContext.GetAllAsync(query);

        Assert.NotNull(result);
        if (setCount > 0) Assert.True(result.Any());
        Assert.True(result.Count() == setCount);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(0, 0, 10)]
    [InlineData(100, 0, 10)]
    [InlineData(1000, 0, 10)]
    [InlineData(10, 5, 10)]
    [InlineData(100, 1, 100)]
    public async Task GetPageAsync_MockedHttpClient_ReturnsPage(int setCount, int offset, int limit)
    {
        var baseUrl = "https://mocked.service";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model?offset={offset}&limit={limit}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(TestModel.GetPage(setCount, offset, limit)));
        });

        var result = await setContext.GetPageAsync(offset, limit);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        if (offset < setCount) Assert.True(result.Data.Any());
        if (offset + limit > setCount)
        {
            var shouldCount = setCount - offset;
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

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(
                TestModel.GetAll(modelCount).FirstOrDefault(x => x.Id == id)));
        });

        var result = await setContext.GetAsync(id);

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

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/specific")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(
                TestModel.GetAll(modelCount).FirstOrDefault(x => x.Id == 1)));
        });

        ((FluxRestSetContext<TestModel, int>)setContext)
            .SetOptions.IdEndpointOptions.GetPathFunc = (key, parameters) => "model/specific";

        var result = await setContext.GetAsync(1);

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

        var setContext = (FluxRestSetContext<TestModel, int>)
            TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/{id}?changeName=False")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(defaultModel));

            x.When($"{baseUrl.TrimEnd('/')}/model/{id}?changeName=True")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(changedModel));
        });

        setContext.SetOptions.IdEndpointOptions.GetPathFunc = (id, parameters) =>
        {
            return $"model/{id}?changeName={parameters!.First()}";
        };

        var resultWithParameterFalse = await setContext.GetAsync(id, false);
        Assert.Equal(defaultName, resultWithParameterFalse.Name);

        var resultWithParameterTrue = await setContext.GetAsync(id, true);
        Assert.Equal(changedName, resultWithParameterTrue.Name);
    }

    [Fact]
    public async Task GetPageAsyncWithParameters_MockedHttpClient_Returns()
    {
        var baseUrl = "https://mocked.service";

        var setContext = (FluxRestSetContext<TestModel, int>)
            TestSetContext.GetTestSetContext(baseUrl, x =>
            {
                x.When($"{baseUrl.TrimEnd('/')}/test-1?offset=0&limit=10")
                .Respond(HttpStatusCode.OK,
                JsonContent.Create(TestModel.GetPage(100, 0, 10)));
            });

        setContext.SetOptions.EndpointOptions.Path = "test-{number}";

        var result = await setContext.GetPageAsync(0, 10, 1);
        Assert.NotNull(result);
        Assert.Equal(10, result.Data!.Count());
    }

    [Fact]
    public async Task GetPageAsyncWithPageEndpoint_MockedHttpClient_Returns()
    {
        var baseUrl = "https://mocked.service";

        var setContext = (FluxRestSetContext<TestModel, int>)
            TestSetContext.GetTestSetContext(baseUrl, x =>
            {
                x.When($"{baseUrl.TrimEnd('/')}/1/test?offset=0&limit=10")
                .Respond(HttpStatusCode.OK,
                JsonContent.Create(TestModel.GetPage(100, 0, 10)));
            });

        setContext.SetOptions.PageEndpointOptions.Path = "{parentId}/test";

        var result = await setContext.GetPageAsync(0, 10, 1);
        Assert.NotNull(result);
        Assert.Equal(10, result.Data!.Count());
    }

    [Fact]
    public async Task AddAsync_MockedHttpClient_ReturnsModel()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;
        var name = "model";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model")
            .Respond(HttpStatusCode.Created,
            JsonContent.Create(new TestModel { Id = id, Name = name }));
        });

        var model = new TestModel { Id = id, Name = name };
        var result = await setContext.AddAsync(model);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task AddAsync_WithResponseType_ReturnsUpdateResponse()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;
        var name = "model";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model")
            .Respond(HttpStatusCode.Created,
            JsonContent.Create(new TestModelUpdateResponse(id, name)));
        });

        var model = new TestModel { Id = id, Name = name };
        var result = await setContext.AddAsync<TestModelUpdateResponse>(model);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task UpdateAsync_MockedHttpClient_ReturnsModel()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;
        var name = "updated model";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(new TestModel { Id = id, Name = name }));
        });

        var model = new TestModel { Id = id, Name = name };
        var result = await setContext.UpdateAsync(id, model);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task UpdateAsync_WithResponseType_ReturnsUpdateResponse()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;
        var name = "updated model";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(new TestModelUpdateResponse { Id = id, Name = name }));
        });

        var model = new TestModel { Id = id, Name = name };
        var result = await setContext.UpdateAsync<TestModelUpdateResponse>(id, model);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task UpdateAsync_Partial_ReturnsModel()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;
        var name = "updated model 1";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(new TestModel { Id = id, Name = name }));
        });

        var model = new TestModel { Id = id, Name = name };
        var result = await setContext.UpdateAsync(id, model, partial: true);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task UpdateAsyncWithResponseType_Partial_ReturnsUpdateResponse()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;
        var name = "updated model 1";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(new TestModelUpdateResponse { Id = id, Name = name }));
        });

        var model = new TestModel { Id = id, Name = "model" };
        var result = await setContext.UpdateAsync<TestModelUpdateResponse>(id, model, partial: true);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task UpdateAsync_IdAsParameter_ReturnsModel()
    {
        var baseUrl = "https://mocked.service";
        var modelId = 1;
        var name = "updated model";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model?id={modelId}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(new TestModel { Id = modelId, Name = name }));
        });

        ((FluxRestSetContext<TestModel, int>)setContext)
            .SetOptions.IdEndpointOptions.GetPathFunc = (_, parameters) => $"model?id={parameters!.First()}";

        var model = new TestModel { Id = modelId, Name = name };
        
        var result = await setContext.UpdateAsync(model, partial: false, modelId);

        Assert.NotNull(result);
        Assert.Equal(modelId, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task UpdateAsync_CustomIdEndpointLogic_ReturnsModel()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;
        var name = "updated model";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/specific/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(new TestModel { Id = id, Name = name }));
        });

        ((FluxRestSetContext<TestModel, int>)setContext)
            .SetOptions.IdEndpointOptions.GetPathFunc = (key, _) => $"model/specific/{key}";

        var model = new TestModel { Id = id, Name = name };

        var result = await setContext.UpdateAsync(id, model);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task UpdateAsyncWithResponseType_IdAsParameter_ReturnsUpdateResponse()
    {
        var baseUrl = "https://mocked.service";
        var modelId = 1;
        var name = "updated model";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model?id={modelId}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(new TestModelUpdateResponse { Id = modelId, Name = name }));
        });

        ((FluxRestSetContext<TestModel, int>)setContext)
            .SetOptions.IdEndpointOptions.GetPathFunc = (_, parameters) => $"model?id={parameters!.First()}";

        var model = new TestModel { Id = modelId, Name = name };

        var result = await setContext.UpdateAsync<TestModelUpdateResponse>(model, partial: false, modelId);

        Assert.NotNull(result);
        Assert.Equal(modelId, result.Id);
        Assert.Equal(name, result.Name);
    }

    [Fact]
    public async Task UpdateAsyncWithResponseType_CustomIdEndpointLogic_ReturnsUpdateResponse()
    {
        var baseUrl = "https://mocked.service";
        var id = 1;
        var name = "updated model";

        var setContext = TestSetContext.GetTestSetContext(baseUrl, x =>
        {
            x.When($"{baseUrl.TrimEnd('/')}/model/specific/{id}")
            .Respond(HttpStatusCode.OK,
            JsonContent.Create(new TestModelUpdateResponse { Id = id, Name = name }));
        });

        ((FluxRestSetContext<TestModel, int>)setContext)
            .SetOptions.IdEndpointOptions.GetPathFunc = (key, _) => $"model/specific/{key}";

        var model = new TestModel { Id = id, Name = name };
        
        var result = await setContext.UpdateAsync<TestModelUpdateResponse>(id, model);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.Name);
    }
}