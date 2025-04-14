using BitzArt.Flux.Builder;
using BitzArt.Flux.Operations;
using BitzArt.Flux.Sets;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.Flux;

public class AddSetContextExtensionTests
{
    private class TestSetContextImplementation<TModel, TKey> : FluxSetContext<TModel, TKey>
        where TModel : class
        where TKey : notnull
    {
        public override Task ExecuteAsync(OperationDescriptor descriptor, Type? responseType, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
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
        serviceBuilder.AddSetContext<TestSetContextImplementation<TestModel, object>>(setName: null, setLifetime: ServiceLifetime.Transient);

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
        serviceBuilder.AddSetContext<TestSetContextImplementation<TestModel, int>>(setName: null, setLifetime: ServiceLifetime.Transient);

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
        var setName = "my-flux-set";

        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);
        var serviceBuilder = fluxBuilder.AddService(serviceName);

        // Act
        serviceBuilder.AddSetContext<TestSetContextImplementation<TestModel, object>>(setName: setName, setLifetime: ServiceLifetime.Transient);

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        FluxSetSignature[] setSignatures =
        [
            // unspecified service name + unspecified set name
            new(null, null),
            // specified service name + unspecified set name
            new(serviceName, null),
            // unspecified service name + specified set name
            new(null, setName),
            // specified service name + specified set name
            new(serviceName, setName)
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
        var setName = "my-flux-set";

        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);
        var serviceBuilder = fluxBuilder.AddService(serviceName);

        // Act
        serviceBuilder.AddSetContext<TestSetContextImplementation<TestModel, int>>(setName: setName, setLifetime: ServiceLifetime.Transient);

        // Assert
        var serviceProvider = services.BuildServiceProvider();

        FluxSetSignature[] setSignatures =
        [
            // unspecified service name + unspecified set name
            new(null, null),
            // specified service name + unspecified set name
            new(serviceName, null),
            // unspecified service name + specified set name
            new(null, setName),
            // specified service name + specified set name
            new(serviceName, setName)
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
}
