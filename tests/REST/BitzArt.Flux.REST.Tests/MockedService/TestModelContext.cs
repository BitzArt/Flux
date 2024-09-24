using Microsoft.Extensions.Logging;
using RichardSzalay.MockHttp;

namespace BitzArt.Flux;

internal static class TestSetContext
{
    public static IFluxSetContext<TestModel> GetTestSetContext(string baseUrl, Action<MockHttpMessageHandler>? configureMockWebApi = null)
    {
        var mockedService = MockedService.GetService(configureMockWebApi);

        var setContext = new FluxRestSetContext<TestModel, int>
            (mockedService.GetHttpClient(),
            new FluxRestServiceOptions(baseUrl),
            LoggerFactory.Create(x => { }).CreateLogger("Flux"),
            new FluxRestSetOptions<TestModel, int>
            {
                Endpoint = "model"
            });

        return setContext;
    }
}
