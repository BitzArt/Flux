namespace BitzArt.Flux.FluentUI.SampleApp;

public static class AddFluxExtension
{
    public static IServiceCollection AddFlux(this IServiceCollection services)
    {
        services.AddFlux(flux =>
        {
            flux.AddService("backend")
                .UsingRest()

                .AddSet<Author>()
                .WithEndpoint("authors")

                .AddSet<Book>()
                .WithPageEndpoint("books{query}");
        });

        return services;
    }
}
