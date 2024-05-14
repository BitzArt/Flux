using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BitzArt.Flux.FluentUI.SampleApp.Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddFlux();

        await builder.Build().RunAsync();
    }
}