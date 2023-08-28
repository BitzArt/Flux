using System.Text.Json;

namespace BitzArt.Communicator;

public class RestCommunicatorServiceOptions
{
    public JsonSerializerOptions SerializerOptions { get; set; }

	public RestCommunicatorServiceOptions()
	{
		SerializerOptions = new JsonSerializerOptions();
	}
}