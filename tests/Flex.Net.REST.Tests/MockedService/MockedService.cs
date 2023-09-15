using RichardSzalay.MockHttp;

namespace Flex;

internal class MockedService
{
    private readonly MockHttpMessageHandler _handler;

	public MockedService(Action<MockHttpMessageHandler> configure)
	{
		_handler = new();
		configure(_handler);
	}

	public HttpClient GetHttpClient() => new(_handler);

	public static MockedService GetService(Action<MockHttpMessageHandler>? configureMockWebApi = null)
	=> new(x =>
	{
        if (configureMockWebApi is not null) configureMockWebApi(x);
	});
}
