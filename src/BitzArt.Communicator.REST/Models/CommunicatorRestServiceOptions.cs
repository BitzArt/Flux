using System.Text.Json;

namespace BitzArt.Communicator;

public class CommunicatorRestServiceOptions
{
    public JsonSerializerOptions SerializerOptions { get; set; }

	public CommunicatorRestServiceOptions()
	{
		SerializerOptions = new JsonSerializerOptions();
	}
}