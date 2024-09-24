using BitzArt.Pagination;
using System.Text.Json.Serialization;

namespace BitzArt.Flux;

internal class TestModel
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    public static IEnumerable<TestModel> GetAll(int count) =>
        Enumerable.Range(1, count)
        .Select(x => new TestModel
        {
            Id = x,
            Name = $"model {x}"
        });

    public static PageResult<TestModel> GetPage(int total, int offset, int limit) => GetPage(total, new PageRequest(offset, limit));
    public static PageResult<TestModel> GetPage(int total, PageRequest request)
        => GetAll(total).ToPage(request);
}
