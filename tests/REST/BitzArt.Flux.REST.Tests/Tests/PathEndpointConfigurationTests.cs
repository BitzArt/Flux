using BitzArt.Flux.REST.Endpoints;
using BitzArt.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux.REST;

public class PathEndpointConfigurationTests
{
    [Fact]
    public void Ctor_AllHttpMethods_ShouldCreateForAllOperationTypes()
    {
        // Arrange
        var serviceConfiguration = new FluxRestServiceConfiguration(null);
        var setConfiguration = new FluxRestSetConfiguration(serviceConfiguration, null);

        // Act
        var endpointConfiguration = new FluxRestPathEndpointConfiguration(setConfiguration, HttpMethods.All, null, false, false);

        // Assert
        Assert.Equal(6, endpointConfiguration.OperationTypes.Count());

        Assert.Contains(typeof(GetOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(GetAllOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(GetPageOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(AddOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(UpdateOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(RemoveOperationDescriptor), endpointConfiguration.OperationTypes);
    }

    private const string ServiceName = "my-service";
    private const string ServiceBaseUrl = "http://some.address";

    [Fact]
    public async Task WithEndpoint_PathString_ShouldBuildGetAllRequestCorrectly()
    {
        // Arrange
        Action<HttpRequestMessage, CancellationToken>? handler = null;
        var delegatingHandler = new TestDelegatingHandler((request, cancellationToken)
            => handler?.Invoke(request, cancellationToken));

        var (serviceProvider, flux) = PrepareTestServices(delegatingHandler,
            services => services.AddSingleton(delegatingHandler),
            s => s
                // Act
                .AddSet<TestEntity, int>(path: null, setKey: "no-path")
                    .WithEndpoint("endpoint")
                .AddSet<TestEntity, int>(path: "set", setKey: "has-path")
                    .WithEndpoint("endpoint"));

        // Assert
        var serviceContext = flux.Service(ServiceName);

        var noPathSet = serviceContext.Set<TestEntity, int>("no-path");
        var hasPathSet = serviceContext.Set<TestEntity, int>("has-path");

        var noPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/endpoint";
        var hasPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/set/endpoint";

        string? url = null;
        HttpMethod? method = null;

        void Cleanup()
        {
            url = null;
            method = null;
        }

        handler = (request, cancellationToken) =>
        {
            url = request.RequestUri?.ToString();
            method = request.Method;
        };

        await noPathSet.ExecuteAsync(new GetAllOperationDescriptor(null));
        Assert.Equal(noPathExpectedEndpoint, url);
        Assert.Equal(HttpMethod.Get, method);

        Cleanup();

        await hasPathSet.ExecuteAsync(new GetAllOperationDescriptor(null));
        Assert.Equal(hasPathExpectedEndpoint, url);
        Assert.Equal(HttpMethod.Get, method);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(100)]
    [InlineData(-100)]
    [InlineData(123456789)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public async Task WithEndpoint_PathString_ShouldBuildGetByIdRequestCorrectly(int id)
    {
        // Arrange
        Action<HttpRequestMessage, CancellationToken>? handler = null;
        var delegatingHandler = new TestDelegatingHandler((request, cancellationToken)
            => handler?.Invoke(request, cancellationToken));

        var (serviceProvider, flux) = PrepareTestServices(delegatingHandler,
            services => services.AddSingleton(delegatingHandler),
        // Act
            s => s
                .AddSet<TestEntity, int>(path: null, setKey: "no-path")
                    .WithEndpoint("endpoint")
                .AddSet<TestEntity, int>(path: "set", setKey: "has-path")
                    .WithEndpoint("endpoint"));

        // Assert
        var serviceContext = flux.Service(ServiceName);

        var noPathSet = serviceContext.Set<TestEntity, int>("no-path");
        var hasPathSet = serviceContext.Set<TestEntity, int>("has-path");

        var noPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/endpoint/{id}";
        var hasPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/set/endpoint/{id}";

        string? url = null;
        HttpMethod? method = null;

        void Cleanup()
        {
            url = null;
            method = null;
        }

        handler = (request, cancellationToken) =>
        {
            url = request.RequestUri?.ToString();
            method = request.Method;
        };

        await noPathSet.ExecuteAsync(new GetOperationDescriptor(id, null));
        Assert.Equal(noPathExpectedEndpoint, url);
        Assert.Equal(HttpMethod.Get, method);

        Cleanup();

        await hasPathSet.ExecuteAsync(new GetOperationDescriptor(id, null));
        Assert.Equal(hasPathExpectedEndpoint, url);
        Assert.Equal(HttpMethod.Get, method);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, 10)]
    [InlineData(null, 100)]
    [InlineData(10, null)]
    [InlineData(100, null)]
    [InlineData(0, 10)]
    [InlineData(0, 100)]
    [InlineData(10, 0)]
    [InlineData(100, 0)]
    [InlineData(0, int.MaxValue)]
    [InlineData(int.MaxValue, 0)]
    [InlineData(int.MaxValue, int.MaxValue)]
    [InlineData(int.MinValue, int.MinValue)]
    public async Task WithEndpoint_PathString_ShouldBuildGetPageRequestCorrectly(int? offset, int? limit)
    {
        // Arrange
        Action<HttpRequestMessage, CancellationToken>? handler = null;
        var delegatingHandler = new TestDelegatingHandler((request, cancellationToken)
            => handler?.Invoke(request, cancellationToken));

        var (serviceProvider, flux) = PrepareTestServices(delegatingHandler,
            services => services.AddSingleton(delegatingHandler),
            s => s
                // Act
                .AddSet<TestEntity, int>(path: null, setKey: "no-path")
                    .WithEndpoint("endpoint")
                .AddSet<TestEntity, int>(path: "set", setKey: "has-path")
                    .WithEndpoint("endpoint"));

        // Assert
        var serviceContext = flux.Service(ServiceName);

        var noPathSet = serviceContext.Set<TestEntity, int>("no-path");
        var hasPathSet = serviceContext.Set<TestEntity, int>("has-path");

        var pageRequest = new PageRequest(offset, limit);

        var noPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/endpoint{pageRequest.ToQueryString().Value}";
        var hasPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/set/endpoint{pageRequest.ToQueryString().Value}";

        string? url = null;
        HttpMethod? method = null;

        void Cleanup()
        {
            url = null;
            method = null;
        }

        handler = (request, cancellationToken) =>
        {
            url = request.RequestUri?.ToString();
            method = request.Method;
        };

        await noPathSet.ExecuteAsync(new GetPageOperationDescriptor(pageRequest, null));
        Assert.Equal(noPathExpectedEndpoint, url);
        Assert.Equal(HttpMethod.Get, method);

        Cleanup();

        await hasPathSet.ExecuteAsync(new GetPageOperationDescriptor(pageRequest, null));
        Assert.Equal(hasPathExpectedEndpoint, url);
        Assert.Equal(HttpMethod.Get, method);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(-1)]
    [InlineData(100)]
    [InlineData(1_000_000)]
    [InlineData(-1_000_000)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public async Task WithEndpoint_PathString_ShouldBuildAddRequestCorrectly(int? id)
    {
        // Arrange
        Action<HttpRequestMessage, CancellationToken>? handler = null;
        var delegatingHandler = new TestDelegatingHandler((request, cancellationToken)
            => handler?.Invoke(request, cancellationToken));

        var (serviceProvider, flux) = PrepareTestServices(delegatingHandler,
            services => services.AddSingleton(delegatingHandler),
            s => s
                // Act
                .AddSet<TestEntity, int>(path: null, setKey: "no-path")
                    .WithEndpoint("endpoint")
                .AddSet<TestEntity, int>(path: "set", setKey: "has-path")
                    .WithEndpoint("endpoint"));

        // Assert
        var serviceContext = flux.Service(ServiceName);

        var noPathSet = serviceContext.Set<TestEntity, int>("no-path");
        var hasPathSet = serviceContext.Set<TestEntity, int>("has-path");

        var hasId = id is not null;
        var idPart = hasId ? $"/{id}" : null;

        var noPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/endpoint{idPart}";
        var hasPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/set/endpoint{idPart}";

        string? url = null;
        HttpMethod? method = null;

        void Cleanup()
        {
            url = null;
            method = null;
        }

        handler = (request, cancellationToken) =>
        {
            url = request.RequestUri?.ToString();
            method = request.Method;
        };

        var entity = new TestEntity();
        var expectedMethod = hasId ? HttpMethod.Put : HttpMethod.Post;

        await noPathSet.ExecuteAsync(new AddOperationDescriptor(id, entity, null));
        Assert.Equal(noPathExpectedEndpoint, url);
        Assert.Equal(expectedMethod, method);

        Cleanup();

        await hasPathSet.ExecuteAsync(new AddOperationDescriptor(id, entity, null));
        Assert.Equal(hasPathExpectedEndpoint, url);
        Assert.Equal(expectedMethod, method);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData(0, false)]
    [InlineData(1, false)]
    [InlineData(10, false)]
    [InlineData(-1, false)]
    [InlineData(100, false)]
    [InlineData(1_000_000, false)]
    [InlineData(-1_000_000, false)]
    [InlineData(int.MaxValue, false)]
    [InlineData(int.MinValue, false)]
    [InlineData(null, true)]
    [InlineData(0, true)]
    [InlineData(1, true)]
    [InlineData(10, true)]
    [InlineData(-1, true)]
    [InlineData(100, true)]
    [InlineData(1_000_000, true)]
    [InlineData(-1_000_000, true)]
    [InlineData(int.MaxValue, true)]
    [InlineData(int.MinValue, true)]
    public async Task WithEndpoint_PathString_ShouldBuildUpdateRequestCorrectly(int? id, bool partial)
    {
        // Arrange
        Action<HttpRequestMessage, CancellationToken>? handler = null;
        var delegatingHandler = new TestDelegatingHandler((request, cancellationToken)
            => handler?.Invoke(request, cancellationToken));

        var (serviceProvider, flux) = PrepareTestServices(delegatingHandler,
            services => services.AddSingleton(delegatingHandler),
            s => s
                // Act
                .AddSet<TestEntity, int>(path: null, setKey: "no-path")
                    .WithEndpoint("endpoint")
                .AddSet<TestEntity, int>(path: "set", setKey: "has-path")
                    .WithEndpoint("endpoint"));

        // Assert
        var serviceContext = flux.Service(ServiceName);

        var noPathSet = serviceContext.Set<TestEntity, int>("no-path");
        var hasPathSet = serviceContext.Set<TestEntity, int>("has-path");

        var hasId = id is not null;
        var idPart = hasId ? $"/{id}" : null;

        var noPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/endpoint{idPart}";
        var hasPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/set/endpoint{idPart}";

        string? url = null;
        HttpMethod? method = null;

        void Cleanup()
        {
            url = null;
            method = null;
        }

        handler = (request, cancellationToken) =>
        {
            url = request.RequestUri?.ToString();
            method = request.Method;
        };

        var entity = new TestEntity();
        var expectedMethod = partial ? HttpMethod.Patch : HttpMethod.Put;

        await noPathSet.ExecuteAsync(new UpdateOperationDescriptor(id, entity, partial, null));
        Assert.Equal(noPathExpectedEndpoint, url);
        Assert.Equal(expectedMethod, method);

        Cleanup();

        await hasPathSet.ExecuteAsync(new UpdateOperationDescriptor(id, entity, partial, null));
        Assert.Equal(hasPathExpectedEndpoint, url);
        Assert.Equal(expectedMethod, method);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(-1)]
    [InlineData(100)]
    [InlineData(1_000_000)]
    [InlineData(-1_000_000)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public async Task WithEndpoint_PathString_ShouldBuildRemoveRequestCorrectly(int? id)
    {
        // Arrange
        Action<HttpRequestMessage, CancellationToken>? handler = null;
        var delegatingHandler = new TestDelegatingHandler((request, cancellationToken)
            => handler?.Invoke(request, cancellationToken));

        var (serviceProvider, flux) = PrepareTestServices(delegatingHandler,
            services => services.AddSingleton(delegatingHandler),
            s => s
                // Act
                .AddSet<TestEntity, int>(path: null, setKey: "no-path")
                    .WithEndpoint("endpoint")
                .AddSet<TestEntity, int>(path: "set", setKey: "has-path")
                    .WithEndpoint("endpoint"));

        // Assert
        var serviceContext = flux.Service(ServiceName);

        var noPathSet = serviceContext.Set<TestEntity, int>("no-path");
        var hasPathSet = serviceContext.Set<TestEntity, int>("has-path");

        var hasId = id is not null;
        var idPart = hasId ? $"/{id}" : null;

        var noPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/endpoint{idPart}";
        var hasPathExpectedEndpoint = $"{ServiceBaseUrl.TrimEnd('/')}/set/endpoint{idPart}";

        string? url = null;
        HttpMethod? method = null;

        void Cleanup()
        {
            url = null;
            method = null;
        }

        handler = (request, cancellationToken) =>
        {
            url = request.RequestUri?.ToString();
            method = request.Method;
        };

        await noPathSet.ExecuteAsync(new RemoveOperationDescriptor(id, null));
        Assert.Equal(noPathExpectedEndpoint, url);
        Assert.Equal(HttpMethod.Delete, method);

        Cleanup();

        await hasPathSet.ExecuteAsync(new RemoveOperationDescriptor(id, null));
        Assert.Equal(hasPathExpectedEndpoint, url);
        Assert.Equal(HttpMethod.Delete, method);
    }

    private record TestServices(IServiceProvider ServiceProvider, IFluxContext FluxContext);

    private static TestServices PrepareTestServices(
        TestDelegatingHandler? handler = null,
        Action<IServiceCollection>? configureServices = null,
        Action<IFluxRestServiceBuilder>? configureFluxService = null)
    {
        var serviceCollection = new ServiceCollection();

        handler ??= new TestDelegatingHandler((request, cancellationToken) => { });
        serviceCollection.AddSingleton(handler);

        configureServices?.Invoke(serviceCollection);

        serviceCollection.AddFlux(flux =>
        {
            var builder = flux.AddService(ServiceName)
                .UsingRest<TestDelegatingHandler>(ServiceBaseUrl);

            configureFluxService?.Invoke(builder);
        });

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var flux = serviceProvider.GetRequiredService<IFluxContext>();

        return new(serviceProvider, flux);
    }
}
