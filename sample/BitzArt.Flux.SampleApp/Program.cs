using BitzArt.Flux.SampleApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace BitzArt.Flux.SampleApp;

internal class Program
{
    private static void Main(string[] args)
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
                .WithPageEndpoint("authors/{authorId}/books");

            x.AddService("library-api")
            .UsingRest("http://your-backend-url")
            .ConfigureHttpClient(client =>
            {
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