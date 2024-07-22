using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace BitzArt.Flux.MudBlazorSample.Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddMudServices();

        builder.Services.AddFlux(builder.HostEnvironment.BaseAddress);

        await builder.Build().RunAsync();
    }
}
