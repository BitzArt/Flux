using Microsoft.Extensions.Logging;
using RichardSzalay.MockHttp;

namespace BitzArt.Flux;

internal static class TestEntityContext
{
    public static IFluxEntityContext<TestEntity> GetTestEntityContext(string baseUrl, Action<MockHttpMessageHandler>? configureMockWebApi = null)
    {
        var mockedService = MockedService.GetService(configureMockWebApi);

        var entityContext = new FluxRestEntityContext<TestEntity, int>
            (mockedService.GetHttpClient(),
            new FluxRestServiceOptions(baseUrl),
            LoggerFactory.Create(x => { }).CreateLogger("Flux"),
            new FluxRestEntityOptions<TestEntity, int>
            {
                Endpoint = "entity"
            });

        return entityContext;
    }
}
