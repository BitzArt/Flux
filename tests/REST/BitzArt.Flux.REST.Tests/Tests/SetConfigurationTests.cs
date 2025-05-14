using BitzArt.Flux.REST.Endpoints;

namespace BitzArt.Flux.REST;

public class SetConfigurationTests
{
    [Theory]
    [InlineData("https://www.library.com", null, "books")]
    public void Add_PathEndpointConfiguration_ShouldAddPathEndpointConfiguration(string? servicePath, string? setPath, string? endpointPath)
    {
        // Arrange
        var serviceConfiguration = new ServiceConfiguration(servicePath);
        var setConfiguration = new TestSetConfiguration(serviceConfiguration, setPath);
        var endpointConfiguration = new PathEndpointConfiguration(serviceConfiguration, setConfiguration, HttpMethods.All, endpointPath);

        var operationTypes = new List<Type>(endpointConfiguration.OperationTypes);

        var endpoint = string.Join("/",
            new List<string?> { servicePath, setPath, endpointPath }
            .Where(x => x is not null)
            .Select(x => x!.TrimEnd('/')));

        // Act
        setConfiguration.Add(endpointConfiguration);

        // Assert
        Assert.Equal(operationTypes, endpointConfiguration.OperationTypes);
    }
}
