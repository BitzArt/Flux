using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BitzArt.Flux.Rest;

public class DelegatingHandlerTests
{
    [Fact]
    public void GetAsync_WithDelegatingHandler_ShouldCallHandler()
    {
        var services = new ServiceCollection();

        var called = false;
        services.AddSingleton(new TestDelegatingHandler(() => called = true));

        services.AddFlux(flux =>
        {
            flux.AddService("test-service")
                .UsingRest<TestDelegatingHandler>("http://test")

                .AddSet<object>();
        });

        var provider = services.BuildServiceProvider();

        var set = provider.GetRequiredService<IFluxSetContext<object>>();
        set.AddAsync<object>(new object());

        Assert.True(called);
    }
}
