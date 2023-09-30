using Microsoft.Extensions.Logging;
using RichardSzalay.MockHttp;

namespace BitzArt.Flux;

internal static class TestModelContext
{
    public static IFluxModelContext<TestModel> GetTestModelContext(string baseUrl, Action<MockHttpMessageHandler>? configureMockWebApi = null)
    {
        var mockedService = MockedService.GetService(configureMockWebApi);

        var modelContext = new FluxRestModelContext<TestModel, int>
            (mockedService.GetHttpClient(),
            new FluxRestServiceOptions(baseUrl),
            LoggerFactory.Create(x => { }).CreateLogger("Flux"),
            new FluxRestModelOptions<TestModel, int>
            {
                Endpoint = "model"
            });

        return modelContext;
    }
}
