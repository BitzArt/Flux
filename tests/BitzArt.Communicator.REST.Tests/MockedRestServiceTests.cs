using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace BitzArt.Communicator;

file class Country
{
    [JsonPropertyName("cca3")]
    public required string CountryCode { get; set; }
}

public class MockedRestServiceTests
{
    [Fact]
    // TODO: Make it a mocked REST service instead of a real WebAPI
    public async Task AddCommunicator_RealService_AbleToRequestAndParseResponse()
    {
        var services = new ServiceCollection();
        services.AddCommunicator(x =>
        {
            x.AddService("RestCountries")
            .UsingRest("https://restcountries.com/v3.1/all?fields=cca3")
            .AddEntity<Country>();
        });

        var serviceProvider = services.BuildServiceProvider();

        var entityCommunicator = serviceProvider.GetRequiredService<IEntityContext<Country>>();
        Assert.NotNull(entityCommunicator);

        var countries = await entityCommunicator.GetAllAsync();
        Assert.NotNull(countries);
        Assert.True(countries.Any());
    }
}