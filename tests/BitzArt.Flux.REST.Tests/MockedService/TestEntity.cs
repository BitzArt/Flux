using BitzArt.Pagination;
using System.Text.Json.Serialization;

namespace BitzArt.Flux;

internal class TestEntity
{
    [JsonPropertyName("id")]
    public required int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    public static IEnumerable<TestEntity> GetAll(int count) =>
        Enumerable.Range(1, count)
        .Select(x => new TestEntity
        {
            Id = x,
            Name = $"Entity {x}"
        });

    public static PageResult<TestEntity> GetPage(int total, int offset, int limit) => GetPage(total, new PageRequest(offset, limit));
    public static PageResult<TestEntity> GetPage(int total, PageRequest request)
        => GetAll(total).ToPage(request);
}
