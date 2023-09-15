namespace Flex;

public interface ICommunicatorRestServiceBuilder : ICommunicatorServiceBuilder
{
    public CommunicatorRestServiceOptions ServiceOptions { get; }
    internal Action<HttpClient>? HttpClientConfiguration { get; set; }
}
