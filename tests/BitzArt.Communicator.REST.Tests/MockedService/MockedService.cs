using RichardSzalay.MockHttp;
using System.Net.Http.Json;
using System.Net;

namespace BitzArt.Communicator;

internal class MockedService
{
    public const string BaseUrl = "https://mockedservice";

    private readonly MockHttpMessageHandler _handler;
	private readonly int EntityCount;

	public MockedService(int entityCount, Action<MockHttpMessageHandler> configure)
	{
		EntityCount = entityCount;
		_handler = new();
		configure(_handler);
	}

	public HttpClient GetHttpClient() => new(_handler)
	{
		BaseAddress = new Uri(BaseUrl)
	};

	public static MockedService GetService(int entityCount, Action<MockHttpMessageHandler>? configureMockWebApi = null)
	=> new(entityCount, x =>
	{
        if (configureMockWebApi is not null) configureMockWebApi(x);
	});
}
