using BitzArt.Flux.SampleApp.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BitzArt.Flux.SampleApp;

internal class Program
{
    private static void Main()
    {
        var services = new ServiceCollection();

        services.AddFlux(x =>
        {
            x.AddService("library-api")
            .UsingRest("http://your-backend-url")

            .AddSet<Author>()
                .WithEndpoint("authors")

            .AddSet<Book>()
                .WithEndpoint("books")
                .WithPageEndpoint("authors/{authorId}/books")

            .ConfigureHttpClient((serviceProvider, client) =>
            {
                // You can get options/services from the service provider here
                // And then configure the HttpClient instance.
                client.DefaultRequestHeaders.Add("X-Api-Key", "MyApiKey");
            })
            .ConfigureJson(json =>
            {
                json.Converters.Add(new JsonStringEnumConverter());
            });
        });

        var serviceProvider = services.BuildServiceProvider();

        var flux = serviceProvider.GetRequiredService<IFluxContext>();
    }
}