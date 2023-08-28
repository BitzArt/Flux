using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BitzArt.Communicator;

file class Country
{
    [JsonPropertyName("CCA3")]
    public required string CountryCode { get; set; }
}

public class MockedRestServiceTests
{
    [Fact]
    // TODO: Make it a mocked REST service instead of a real WebAPI
    public void AddCommunicator_RealService_AbleToRequestAndParseResponse()
    {
        var services = new ServiceCollection();
        services.AddCommunicator(x =>
        {
            x.AddService("RestCountries")
            .UsingRest("https://restcountries.com/v3.1/all")
            .AddEntity<Country>();
        });

        var serviceProvider = services.BuildServiceProvider();

        var entityCommunicator = serviceProvider.GetRequiredService<IEntityCommunicator<Country>>();
        Assert.NotNull(entityCommunicator);


    }
}