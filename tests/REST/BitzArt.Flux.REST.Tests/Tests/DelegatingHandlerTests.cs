using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BitzArt.Flux.REST;

public class DelegatingHandlerTests
{
    [Fact]
    public void GetAsync_WithDelegatingHandler_ShouldCallHandler()
    {
        var services = new ServiceCollection();

        var testHandler = new TestHandler();
        services.AddSingleton(testHandler);

        services.AddFlux(flux =>
        {
            flux.AddService("test-service")
                .UsingRest<TestHandler>("http://test")

                .AddSet<object>();
        });

        var provider = services.BuildServiceProvider();

        var set = provider.GetRequiredService<IFluxSetContext<object>>();
        set.AddAsync<object>(new object());

        Assert.True(testHandler.Called);
    }

    private class TestHandler : DelegatingHandler
    {
        public bool Called { get; private set; } = false;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Called = true;
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
