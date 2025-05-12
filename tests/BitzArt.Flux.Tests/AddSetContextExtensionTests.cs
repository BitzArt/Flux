using BitzArt.Flux.Builder;
using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class AddSetContextExtensionTests
{
    private class TestSetContextImplementation<TModel, TKey> : FluxSetContext<TModel, TKey, TestSetContextConfiguration>
        where TModel : class
        where TKey : notnull
    {
        public TestSetContextImplementation(TestSetContextConfiguration config) : base(config) { }

        public override Task ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }

    private record TestSetContextConfiguration
    {
        public string? Text { get; private init; }

        public TestSetContextConfiguration(string? text = null)
        {
            Text = text;
        }
    }

    private class TestModel { }

    [Fact]
    public void AddSetContext_KeylessNameless_ShouldRegisterSetContext()
    {
        // Arrange
        var serviceName = "my-flux-service";

        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);
        var serviceBuilder = fluxBuilder.AddService(serviceName);

        // Act
        services.AddSetContext(serviceName, setKey: null, setLifetime: ServiceLifetime.Transient, sp
            => new TestSetContextImplementation<TestModel, object>(new()));

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        FluxSetSignature[] setSignatures =
        [
            // unspecified service name + unspecified set name
            new(null, null),
            // specified service name + unspecified set name
            new(serviceName, null)
        ];

        Assert.All(setSignatures, setSignature =>
        {
            // ---------------------------------------------

            var keyUnspecifiedSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel>>>(setSignature);

            Assert.NotNull(keyUnspecifiedSetContexts);
            Assert.Single(keyUnspecifiedSetContexts);
            var keyUnspecifiedSetContext = keyUnspecifiedSetContexts.First();
            Assert.IsType<FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, object>>(keyUnspecifiedSetContext);

            // ---------------------------------------------

            var keySpecifiedSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel, object>>>(setSignature);

            Assert.NotNull(keySpecifiedSetContexts);
            Assert.Single(keySpecifiedSetContexts);
            var keySpecifiedSetContext = keySpecifiedSetContexts.First();
            Assert.IsType<TestSetContextImplementation<TestModel, object>>(keySpecifiedSetContext);

            // ---------------------------------------------
        });
    }

    [Fact]
    public void AddSetContext_KeyedNameless_ShouldRegisterSetContext()
    {
        // Arrange
        var serviceName = "my-flux-service";

        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);
        var serviceBuilder = fluxBuilder.AddService(serviceName);

        // Act
        services.AddSetContext(serviceName, setKey: null, setLifetime: ServiceLifetime.Transient, sp
            => new TestSetContextImplementation<TestModel, int>(new()));

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        FluxSetSignature[] setSignatures =
        [
            // unspecified service name + unspecified set name
            new(null, null),
            // specified service name + unspecified set name
            new(serviceName, null)
        ];

        Assert.All(setSignatures, setSignature =>
        {
            // ---------------------------------------------

            var keySpecifiedSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel, int>>>(setSignature);

            Assert.NotNull(keySpecifiedSetContexts);
            Assert.Single(keySpecifiedSetContexts);
            var keySpecifiedSetContext = keySpecifiedSetContexts.First();
            Assert.IsType<TestSetContextImplementation<TestModel, int>>(keySpecifiedSetContext);

            // ---------------------------------------------

            var keyUnspecifiedSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel>>>(setSignature);

            Assert.NotNull(keyUnspecifiedSetContexts);
            Assert.Single(keyUnspecifiedSetContexts);
            var keyUnspecifiedSetContext = keyUnspecifiedSetContexts.First();
            Assert.IsType<FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, int>>(keyUnspecifiedSetContext);
            var keyUnspecifiedWrapper = (FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, int>)keyUnspecifiedSetContext;
            Assert.IsType<TestSetContextImplementation<TestModel, int>>(keyUnspecifiedWrapper.InnerSetContext);

            // ---------------------------------------------

            var keyExplicitlyObjectSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel, object>>>(setSignature);

            Assert.NotNull(keyExplicitlyObjectSetContexts);
            Assert.Single(keyExplicitlyObjectSetContexts);
            var keyExplicitlyObjectSetContext = keyExplicitlyObjectSetContexts.First();
            Assert.IsType<FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, int>>(keyExplicitlyObjectSetContext);
            var keyExplicitlyObjectWrapper = (FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, int>)keyExplicitlyObjectSetContext;
            Assert.IsType<TestSetContextImplementation<TestModel, int>>(keyExplicitlyObjectWrapper.InnerSetContext);

            // ---------------------------------------------
        });
    }

    [Fact]
    public void AddSetContext_KeylessNamed_ShouldRegisterSetContext()
    {
        // Arrange
        var serviceName = "my-flux-service";
        var setKey = "my-flux-set";

        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);
        var serviceBuilder = fluxBuilder.AddService(serviceName);

        // Act
        services.AddSetContext(serviceName, setKey: setKey, setLifetime: ServiceLifetime.Transient, sp
            => new TestSetContextImplementation<TestModel, object>(new()));

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        FluxSetSignature[] setSignatures =
        [
            // unspecified service name + unspecified set name
            new(null, null),
            // specified service name + unspecified set name
            new(serviceName, null),
            // unspecified service name + specified set name
            new(null, setKey),
            // specified service name + specified set name
            new(serviceName, setKey)
        ];

        Assert.All(setSignatures, setSignature =>
        {
            // ---------------------------------------------

            var keyUnspecifiedSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel>>>(setSignature);

            Assert.NotNull(keyUnspecifiedSetContexts);
            Assert.Single(keyUnspecifiedSetContexts);
            var keyUnspecifiedSetContext = keyUnspecifiedSetContexts.First();
            Assert.IsType<FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, object>>(keyUnspecifiedSetContext);

            // ---------------------------------------------

            var keySpecifiedSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel, object>>>(setSignature);

            Assert.NotNull(keySpecifiedSetContexts);
            Assert.Single(keySpecifiedSetContexts);
            var keySpecifiedSetContext = keySpecifiedSetContexts.First();
            Assert.IsType<TestSetContextImplementation<TestModel, object>>(keySpecifiedSetContext);

            // ---------------------------------------------
        });
    }

    [Fact]
    public void AddSetContext_KeyedNamed_ShouldRegisterSetContext()
    {
        // Arrange
        var serviceName = "my-flux-service";
        var setKey = "my-flux-set";

        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);
        var serviceBuilder = fluxBuilder.AddService(serviceName);

        // Act
        services.AddSetContext(serviceName, setKey: setKey, setLifetime: ServiceLifetime.Transient, sp
            => new TestSetContextImplementation<TestModel, int>(new()));

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        FluxSetSignature[] setSignatures =
        [
            // unspecified service name + unspecified set name
            new(null, null),
            // specified service name + unspecified set name
            new(serviceName, null),
            // unspecified service name + specified set name
            new(null, setKey),
            // specified service name + specified set name
            new(serviceName, setKey)
        ];

        Assert.All(setSignatures, setSignature =>
        {
            // ---------------------------------------------

            var keySpecifiedSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel, int>>>(setSignature);

            Assert.NotNull(keySpecifiedSetContexts);
            Assert.Single(keySpecifiedSetContexts);
            var keySpecifiedSetContext = keySpecifiedSetContexts.First();
            Assert.IsType<TestSetContextImplementation<TestModel, int>>(keySpecifiedSetContext);

            // ---------------------------------------------

            var keyUnspecifiedSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel>>>(setSignature);

            Assert.NotNull(keyUnspecifiedSetContexts);
            Assert.Single(keyUnspecifiedSetContexts);
            var keyUnspecifiedSetContext = keyUnspecifiedSetContexts.First();
            Assert.IsType<FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, int>>(keyUnspecifiedSetContext);
            var keyUnspecifiedWrapper = (FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, int>)keyUnspecifiedSetContext;
            Assert.IsType<TestSetContextImplementation<TestModel, int>>(keyUnspecifiedWrapper.InnerSetContext);

            // ---------------------------------------------

            var keyExplicitlyObjectSetContexts = serviceProvider.GetKeyedService<IEnumerable<IFluxSetContext<TestModel, object>>>(setSignature);

            Assert.NotNull(keyExplicitlyObjectSetContexts);
            Assert.Single(keyExplicitlyObjectSetContexts);
            var keyExplicitlyObjectSetContext = keyExplicitlyObjectSetContexts.First();
            Assert.IsType<FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, int>>(keyExplicitlyObjectSetContext);
            var keyExplicitlyObjectWrapper = (FluxSetContextUnspecifiedKeyTypeWrapper<TestModel, int>)keyExplicitlyObjectSetContext;
            Assert.IsType<TestSetContextImplementation<TestModel, int>>(keyExplicitlyObjectWrapper.InnerSetContext);

            // ---------------------------------------------
        });
    }

    [Fact]
    public void AddSetContext_KeylessNameless_ForwardsConfiguration()
    {
        // Arrange
        var serviceName = "my-flux-service";
        var setKey = "my-flux-set";
        var testText = "some-text";

        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);
        var serviceBuilder = fluxBuilder.AddService(serviceName);

        var configuration = new TestSetContextConfiguration(testText);

        // Act
        services.AddSetContext(serviceName, setKey: setKey, setLifetime: ServiceLifetime.Transient, sp
            => new TestSetContextImplementation<TestModel, object>(configuration));

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var setContext = serviceProvider.GetRequiredService<IFluxSetContext<TestModel, object>>();

        Assert.NotNull(setContext);

        Assert.IsType<TestSetContextImplementation<TestModel, object>>(setContext);
        var testSetContext = (TestSetContextImplementation<TestModel, object>)setContext;
        
        Assert.Equal(testText, testSetContext.Configuration.Text);
    }
}
