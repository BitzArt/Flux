namespace BitzArt.Flux.FluentUI.SampleApp;

public static class AddFluxExtension
{
    public static IServiceCollection AddFlux(this IServiceCollection services, string baseUrl)
    {
        services.AddFlux(flux =>
        {
            flux.AddService("backend")
                .UsingRest(baseUrl)

                .AddSet<Author>()
                .WithEndpoint("authors")

                .AddSet<Book>()
                .WithPageEndpoint("books{query}");
        });

        return services;
    }
}
