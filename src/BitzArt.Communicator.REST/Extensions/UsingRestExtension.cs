using BitzArt.Communicator;

namespace BitzArt;

public static class UsingRestExtension
{
    public static IRestCommunicatorServiceBuilder UsingRest(this ICommunicatorServicePreBuilder prebuilder, string? baseUrl = null)
    {
        return new RestCommunicatorServiceBuilder(prebuilder, baseUrl);
    }
}
