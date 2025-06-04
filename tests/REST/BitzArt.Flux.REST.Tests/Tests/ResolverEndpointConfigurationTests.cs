using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

public class ResolverEndpointConfigurationTests
{
    [Fact]
    public async Task WithEndpoint_WithHttpRequestMessageResolver_ShouldUseResolverForMatchingOperations()
    {
        // Arrange
        var resolverCalled = false;

        var serviceCollection = new ServiceCollection();
        HttpRequestMessage? request = null;
        var delegatingHandler = new TestDelegatingHandler((req) => request = req);
        serviceCollection.AddSingleton(delegatingHandler);

        var uri = new Uri("http://some-path");

        // Act
        serviceCollection.AddFlux(flux =>
        {
            flux.AddService("test-service")
                .UsingRest<TestDelegatingHandler>()
                    .AddSet<TestEntity, int>()
                        .WithEndpoint((int id) =>
                        {
                            resolverCalled = true;
                            return new HttpRequestMessage(HttpMethod.Get, uri);
                        });
        });

        // Assert
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var set = serviceProvider.GetRequiredService<IFluxSetContext<TestEntity, int>>();

        Assert.False(resolverCalled);
        await set.ExecuteAsync(new GetOperationDescriptor(id: 1, null));
        Assert.True(resolverCalled);
        Assert.NotNull(request);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal(uri, request.RequestUri);
    }

    [Fact]
    public async Task WithEndpoint_ResolverAfterPathConfiguration_ShouldOverrideAndUseResolverForMatchingOperations()
    {
        // Arrange
        var resolverCalled = false;

        var serviceCollection = new ServiceCollection();
        HttpRequestMessage? request = null;
        var delegatingHandler = new TestDelegatingHandler((req) => request = req);
        serviceCollection.AddSingleton(delegatingHandler);

        var uri = new Uri("http://some-path");

        // Act
        serviceCollection.AddFlux(flux =>
        {
            flux.AddService("test-service")
                .UsingRest<TestDelegatingHandler>()
                    .AddSet<TestEntity, int>()
                        .WithEndpoint("wrong") // this should be overridden
                        .WithEndpoint((int id) =>
                        {
                            resolverCalled = true;
                            return new HttpRequestMessage(HttpMethod.Get, uri);
                        });
        });

        // Assert
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var flux = serviceProvider.GetRequiredService<IFluxContext>();
        var set = flux.Set<TestEntity, int>();

        Assert.False(resolverCalled);
        await set.ExecuteAsync(new GetOperationDescriptor(id: 1, null));
        Assert.True(resolverCalled);
        Assert.NotNull(request);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal(uri, request.RequestUri);
    }

    [Fact]
    public async Task WithEndpoint_ResolverBeforePathConfiguration_ShouldUseResolverAndIgnorePathForMatchingOperations()
    {
        // Arrange
        var resolverCalled = false;

        var serviceCollection = new ServiceCollection();
        HttpRequestMessage? request = null;
        var delegatingHandler = new TestDelegatingHandler((req) => request = req);
        serviceCollection.AddSingleton(delegatingHandler);

        var uri = new Uri("http://some-path");

        // Act
        serviceCollection.AddFlux(flux =>
        {
            flux.AddService("test-service")
                .UsingRest<TestDelegatingHandler>()
                    .AddSet<TestEntity, int>()
                        .WithEndpoint((int id) =>
                        {
                            resolverCalled = true;
                            return new HttpRequestMessage(HttpMethod.Get, uri);
                        })
                        .WithEndpoint("wrong"); // this should be ignored;
        });

        // Assert
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var flux = serviceProvider.GetRequiredService<IFluxContext>();
        var set = flux.Set<TestEntity, int>();

        Assert.False(resolverCalled);
        await set.ExecuteAsync(new GetOperationDescriptor(id: 1, null));
        Assert.True(resolverCalled);
        Assert.NotNull(request);
        Assert.Equal(HttpMethod.Get, request.Method);
        Assert.Equal(uri, request.RequestUri);
    }
}
