namespace BitzArt.Communicator;

public interface IRestCommunicatorServiceBuilder : ICommunicatorServiceBuilder
{
    public RestCommunicatorServiceOptions Options { get; }
    internal Action<HttpClient>? HttpClientConfiguration { get; set; }
}
