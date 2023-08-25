using BitzArt.Communicator;

namespace BitzArt;

public static class AddServiceExtension
{
    public static ICommunicatorServicePreBuilder AddService(this ICommunicatorBuilder builder, string? name = null)
    {
        var service = new CommunicatorServicePreBuilder(builder.Factory, name);

        return service;
    }
}
