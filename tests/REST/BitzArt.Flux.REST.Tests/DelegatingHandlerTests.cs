using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace BitzArt.Flux;

public class DelegatingHandlerTests
{
    [Fact]
    public void GetAsync_WithDelegatingHandler_CallsHandler()
    {
        var services = new ServiceCollection();

        var testHandler = new TestHandler();
        services.AddSingleton(testHandler);

        services.AddFlux(flux =>
        {
            flux.AddService("test-service")
                .UsingRest<TestHandler>("http://test")

                .AddSet<TestModel>();
        });

        var provider = services.BuildServiceProvider();

        var set = provider.GetRequiredService<IFluxSetContext<TestModel>>();
        set.AddAsync<object>(new TestModel
        {
            Id = 1,
            Name = ""
        });

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
