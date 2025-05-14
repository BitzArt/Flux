using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

public class PathEndpointConfigurationTests
{
    [Fact]
    public void Ctor_AllHttpMethods_ShouldCreateForAllOperationTypes()
    {
        // Arrange
        var serviceConfiguration = new ServiceConfiguration(null);
        var setConfiguration = new TestSetConfiguration(serviceConfiguration, null);

        // Act
        var endpointConfiguration = new PathEndpointConfiguration(serviceConfiguration, setConfiguration, HttpMethods.All, null);

        // Assert
        Assert.Equal(6, endpointConfiguration.OperationTypes.Count());

        Assert.Contains(typeof(GetOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(GetAllOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(GetPageOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(AddOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(UpdateOperationDescriptor), endpointConfiguration.OperationTypes);
        Assert.Contains(typeof(RemoveOperationDescriptor), endpointConfiguration.OperationTypes);
    }
}
