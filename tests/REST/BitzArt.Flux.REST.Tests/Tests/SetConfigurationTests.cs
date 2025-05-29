using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

public class SetConfigurationTests
{
    private class TestEndpointConfiguration : FluxRestEndpointConfiguration
    {
        private readonly Func<OperationDescriptor, IServiceProvider, HttpRequestMessage> _resolver;

        public TestEndpointConfiguration(
            FluxRestSetConfiguration setConfiguration,
            HttpMethods httpMethods = HttpMethods.All,
            Func<OperationDescriptor, IServiceProvider, HttpRequestMessage>? resolver = null)
            : base(setConfiguration, httpMethods)
        {
            resolver ??= (_, _) => new();
            _resolver = resolver;
        }

        public override IEnumerable<Type> OperationTypes => OperationDescriptor.Types.Concrete;

        public override HttpRequestMessage Resolve(OperationDescriptor descriptor, IServiceProvider serviceProvider)
            => _resolver.Invoke(descriptor, serviceProvider);
    }

    [Fact]
    public void Add_TestEndpointConfiguration_ShouldUseResolver()
    {
        // Arrange
        var setConfiguration = new FluxRestSetConfiguration(new FluxRestServiceConfiguration(null), null);

        bool resolverCalled = false;
        var requestMessage = new HttpRequestMessage();

        var endpointConfiguration = new TestEndpointConfiguration(setConfiguration, resolver: (operation, serviceProvider) =>
        {
            resolverCalled = true;

            return requestMessage;
        });

        // Act
        setConfiguration.Add(endpointConfiguration);

        // Assert
        var operation = new GetOperationDescriptor(id: null, null);
        Assert.False(resolverCalled);
        var result = setConfiguration.Resolve(operation, null!);
        Assert.True(resolverCalled);
        Assert.Equal(requestMessage, result);
    }

    [Fact]
    public void Replace_WithHttpMethodsSubset_ShouldReplace()
    {
        // Arrange
        var setConfiguration = new FluxRestSetConfiguration(new FluxRestServiceConfiguration(null), null);

        bool configuration1Called = false;
        var configuration1RequestMessage = new HttpRequestMessage();

        var endpointConfiguration1 = new TestEndpointConfiguration(setConfiguration, HttpMethods.Get | HttpMethods.Post, (_, _) =>
        {
            configuration1Called = true;
            return configuration1RequestMessage;
        });

        bool configuration2Called = false;
        var configuration2RequestMessage = new HttpRequestMessage();

        var endpointConfiguration2 = new TestEndpointConfiguration(setConfiguration, HttpMethods.Get, (_, _) =>
        {
            configuration2Called = true;
            return configuration2RequestMessage;
        });

        // Act
        setConfiguration.Add(endpointConfiguration1);
        setConfiguration.Add(endpointConfiguration2);

        // Assert
        var requestMessage = setConfiguration.Resolve(new GetOperationDescriptor(id: null, null), null!);

        Assert.False(configuration1Called);
        Assert.True(configuration2Called);

        Assert.NotEqual(configuration1RequestMessage, requestMessage);
        Assert.Equal(configuration2RequestMessage, requestMessage);
    }

    [Fact]
    public void Replace_WithHttpMethodsNotSubset_ShouldNotReplace()
    {
        // Arrange
        var setConfiguration = new FluxRestSetConfiguration(new FluxRestServiceConfiguration(null), null);

        bool configuration1Called = false;
        var configuration1RequestMessage = new HttpRequestMessage();

        var endpointConfiguration1 = new TestEndpointConfiguration(setConfiguration, HttpMethods.Get, (_, _) =>
        {
            configuration1Called = true;
            return configuration1RequestMessage;
        });

        bool configuration2Called = false;
        var configuration2RequestMessage = new HttpRequestMessage();

        var endpointConfiguration2 = new TestEndpointConfiguration(setConfiguration, HttpMethods.Get | HttpMethods.Post, (_, _) =>
        {
            configuration2Called = true;
            return configuration2RequestMessage;
        });

        // Act
        setConfiguration.Add(endpointConfiguration1);
        setConfiguration.Add(endpointConfiguration2);

        // Assert
        var requestMessage = setConfiguration.Resolve(new GetOperationDescriptor(id: null, null), null!);

        Assert.False(configuration2Called);
        Assert.True(configuration1Called);

        Assert.NotEqual(configuration2RequestMessage, requestMessage);
        Assert.Equal(configuration1RequestMessage, requestMessage);
    }
}
