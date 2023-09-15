using Microsoft.Extensions.Logging;
using RichardSzalay.MockHttp;

namespace Flex;

internal static class TestEntityContext
{
    public static ICommunicationContext<TestEntity> GetTestEntityContext(string baseUrl, Action<MockHttpMessageHandler>? configureMockWebApi = null)
    {
        var mockedService = MockedService.GetService(configureMockWebApi);

        var entityContext = new CommunicatorRestEntityContext<TestEntity, int>
            (mockedService.GetHttpClient(),
            new CommunicatorRestServiceOptions(baseUrl),
            LoggerFactory.Create(x => { }).CreateLogger("Communicator"),
            new CommunicatorRestEntityOptions<TestEntity, int>
            {
                Endpoint = "entity"
            });

        return entityContext;
    }
}
