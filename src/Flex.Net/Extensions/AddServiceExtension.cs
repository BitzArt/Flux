using BitzArt.Communicator;

namespace BitzArt;

public static class AddServiceExtension
{
    public static ICommunicatorServicePreBuilder AddService(this ICommunicatorBuilder builder, string name)
    {
        var service = new CommunicatorServicePreBuilder(builder.Services, builder.Factory, name);

        return service;
    }
}
