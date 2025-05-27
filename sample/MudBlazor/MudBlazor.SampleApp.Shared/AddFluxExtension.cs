using BitzArt.Flux;
using BitzArt.Flux.MudBlazor;
using BitzArt.Pagination;
using Microsoft.Extensions.DependencyInjection;

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
                    .WithEndpoint((GetPageOperationDescriptor operation) => $"books{operation.GetQueryString()}");
        });

        services.AddFluxSetDataProvider<Book>();
    }
}
