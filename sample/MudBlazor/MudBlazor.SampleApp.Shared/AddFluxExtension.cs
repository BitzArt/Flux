using Microsoft.Extensions.DependencyInjection;
using BitzArt.Flux;
using BitzArt.Flux.MudBlazor;

namespace MudBlazor.SampleApp;

public static class AddFluxExtension
{
    public static void AddFlux(this IServiceCollection services, string baseUrl)
    {
        services.AddFlux(x =>
        {
            x.AddService("library-web-api")
            .UsingRest(baseUrl.TrimEnd('/') + "/api")
            .AddSet<Author, int>()
                .WithEndpoint("authors")
            .AddSet<Book, int>()
                .WithEndpoint("books")
                .WithPageEndpoint("books{query}");
        });

        services.AddFluxSetDataProvider<Book>();
    }
}
