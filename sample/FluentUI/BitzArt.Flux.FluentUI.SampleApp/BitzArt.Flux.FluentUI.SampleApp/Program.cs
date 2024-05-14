using BitzArt.Blazor.MVVM;
using BitzArt.Flux.FluentUI.SampleApp.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace BitzArt.Flux.FluentUI.SampleApp;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
        builder.Services.AddFluentUIComponents();

        builder.Services.AddFlux("http://localhost:8080/api");
        builder.Services.AddBlazorViewModels();
        builder.Services.AddRenderingEnvironment();

        builder.Services.AddFluentUIComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
        }

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.MapWebApiEndpoints();

        app.Run();
    }
}