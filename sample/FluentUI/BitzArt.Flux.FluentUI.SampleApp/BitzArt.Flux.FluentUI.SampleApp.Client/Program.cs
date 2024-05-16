using BitzArt.Blazor.MVVM;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp.Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        var baseAddress = builder.HostEnvironment.BaseAddress.TrimEnd('/') + "/api";

        builder.Services.AddFlux(baseAddress);
        builder.Services.AddBlazorViewModels();
        builder.Services.AddItemsProviders();
        builder.Services.AddRenderingEnvironment();

        builder.Services.AddFluentUIComponents();

        await builder.Build().RunAsync();
    }
}