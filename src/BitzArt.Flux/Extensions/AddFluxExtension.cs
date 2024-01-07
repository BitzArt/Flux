using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

/// <summary>
/// Adds Flux to the IServiceCollection.
/// See <see href="https://bitzart.github.io/Flux/02.configure.html">Configure Flux</see> for more information.
/// </summary>
public static class AddFluxExtension
{
    public static IServiceCollection AddFlux(this IServiceCollection services, Action<IFluxBuilder> configure)
    {
        var alreadyRegistered = services
            .Any(x => x.Lifetime == ServiceLifetime.Singleton &&
            x.ServiceType == typeof(IFluxFactory));

        if (alreadyRegistered) throw new FluxAlreadyRegisteredException();

        var builder = new FluxBuilder(services);
        configure(builder);

        var provider = builder.Factory;
        services.AddSingleton(provider);

        services.AddScoped<IFluxContext>(x => new FluxContext(provider, x));

        return services;
    }

    private class FluxAlreadyRegisteredException : Exception
    {
        private const string Msg = "Flux is already registered in this IServiceCollection";
        public FluxAlreadyRegisteredException() : base(Msg) { }
    }
}
