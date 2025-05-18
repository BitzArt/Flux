using System.Net;

namespace BitzArt.Flux.REST;

internal class TestDelegatingHandler : DelegatingHandler
{
    private Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handler;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _handler.Invoke(request, cancellationToken);
    }

    public TestDelegatingHandler(Action handler)
        : this((_, _) => handler.Invoke()) { }

    public TestDelegatingHandler(Action<HttpRequestMessage> handler)
        : this((request, _) => handler.Invoke(request)) { }

    public TestDelegatingHandler(Action<HttpRequestMessage, CancellationToken> handler)
        : this((req, token) =>
        {
            handler(req, token);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        })
    { }

    public TestDelegatingHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> handler)
        : this((request, _) => handler.Invoke(request)) { }

    public TestDelegatingHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
        : base()
    {
        _handler = handler;
    }
}
