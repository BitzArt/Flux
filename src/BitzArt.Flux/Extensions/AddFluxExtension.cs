using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public static class AddFluxExtension
{
    public static IServiceCollection AddFlux(this IServiceCollection services, Action<IFluxBuilder> configure)
    {
        var alreadyRegistered = services
            .Any(x => x.Lifetime == ServiceLifetime.Singleton &&
            x.ServiceType == typeof(IFluxProvider));

        if (alreadyRegistered) throw new FluxAlreadyRegisteredException();

        var builder = new FluxBuilder(services);
        configure(builder);

        var factory = builder.Factory;
        services.AddSingleton(factory);

        services.AddScoped<IFlux>(x => new Flux(x));

        return services;
    }

    private class FluxAlreadyRegisteredException : Exception
    {
        private const string Msg = "Flux is already registered in this IServiceCollection";
        public FluxAlreadyRegisteredException() : base(Msg) { }
    }
}
