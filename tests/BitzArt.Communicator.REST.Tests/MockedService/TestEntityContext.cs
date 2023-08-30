using Microsoft.Extensions.Logging;
using RichardSzalay.MockHttp;

namespace BitzArt.Communicator;

internal static class TestEntityContext
{
    public static ICommunicationContext<TestEntity> GetTestEntityContext(int entityCount, Action<MockHttpMessageHandler>? configureMockWebApi = null)
    {
        var mockedService = MockedService.GetService(entityCount, configureMockWebApi);

        var entityContext = new CommunicatorRestEntityContext<TestEntity, int>
            (mockedService.GetHttpClient(),
            new CommunicatorRestServiceOptions(MockedService.BaseUrl),
            LoggerFactory.Create(x => { }).CreateLogger("Communicator"),
            new CommunicatorRestEntityOptions<TestEntity, int>
            {
                Endpoint = "entity"
            });

        return entityContext;
    }
}
