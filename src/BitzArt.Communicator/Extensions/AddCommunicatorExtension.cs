using BitzArt.Communicator;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt;

public static class AddCommunicatorExtension
{
    public static IServiceCollection AddCommunicator(this IServiceCollection services, Action<ICommunicatorBuilder> configure)
    {
        var builder = new CommunicatorBuilder();
        configure(builder);

        services.AddSingleton(builder.Factory);

        return services;
    }
}
