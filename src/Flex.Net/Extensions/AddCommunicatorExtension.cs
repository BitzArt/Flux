using Microsoft.Extensions.DependencyInjection;

namespace Flex;

public static class AddCommunicatorExtension
{
    public static IServiceCollection AddCommunicator(this IServiceCollection services, Action<ICommunicatorBuilder> configure)
    {
        var alreadyRegistered = services
            .Any(x => x.Lifetime == ServiceLifetime.Singleton &&
            x.ServiceType == typeof(ICommunicatorServiceFactory));

        if (alreadyRegistered) throw new CommunicatorAlreadyRegisteredException();

        var builder = new CommunicatorBuilder(services);
        configure(builder);

        var factory = builder.Factory;
        services.AddSingleton(factory);

        services.AddScoped<ICommunicationContext>(x => new CommunicationContext(x));

        return services;
    }

    private class CommunicatorAlreadyRegisteredException : Exception
    {
        private const string Msg = "Communicator is already registered for this IServiceCollection";
        public CommunicatorAlreadyRegisteredException() : base(Msg) { }
    }
}
