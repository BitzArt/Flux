using System.Text.Json.Serialization;

namespace BitzArt.Flux;

internal class TestModel
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    public TestModel() { }
    public TestModel(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
