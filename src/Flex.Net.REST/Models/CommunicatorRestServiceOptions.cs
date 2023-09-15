using System.Text.Json;

namespace BitzArt.Communicator;

public class CommunicatorRestServiceOptions
{
	public string? BaseUrl { get; set; }
    public JsonSerializerOptions SerializerOptions { get; set; }

    public CommunicatorRestServiceOptions(string? baseUrl)
	{
		BaseUrl = baseUrl;
		SerializerOptions = new JsonSerializerOptions();
	}
}