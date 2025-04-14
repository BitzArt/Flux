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
    public void AddSetContext_OnEmptyService_ShouldRegisterSetContext()
    {
        // Arrange
        var serviceName = "TestService";

        var services = new ServiceCollection();
        var fluxBuilder = new FluxBuilder(services);
        var serviceBuilder = fluxBuilder.AddService(serviceName);

        // Act
        serviceBuilder.AddSetContext<TestSetContextImplementation<TestModel, object>>(setName: null, setLifetime: ServiceLifetime.Transient);

        // Assert

    }
}
