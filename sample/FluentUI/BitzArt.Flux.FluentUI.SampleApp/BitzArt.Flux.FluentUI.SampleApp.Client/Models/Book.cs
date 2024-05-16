using System.Text.Json.Serialization;

namespace BitzArt.Flux.FluentUI.SampleApp;

public class Book
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("authorId")]
    public int? AuthorId { get; set; }

    [JsonPropertyName("rating")]
    public BookRatingInfo? Rating { get; set; }

    public Book() { }

    public Book(int id, string title, int authorId)
    {
        Id = id;
        Title = title;
        AuthorId = authorId;
    }

    public Book(int id, string title, int authorId, double rating, int ratingCount)
        : this(id, title, authorId)
    {
        Rating = new BookRatingInfo(rating, ratingCount);
    }

    private static readonly Random _random = new(42069);

    private static double GetRandomRating() => _random.NextDouble() * 4 + 1;

    private static int GetRandomRatingCount() => _random.Next(1, 1000);

    public static Book New(int id, string title, int authorId) =>
        new(id, title, authorId, GetRandomRating(), GetRandomRatingCount());
}

public class BookRatingInfo
{
    [JsonPropertyName("rating")]
    public double? Rating { get; set; }

    [JsonPropertyName("ratingCount")]
    public int? RatingCount { get; set; }

    public BookRatingInfo() { }

    public BookRatingInfo(double rating, int ratingCount)
    {
        Rating = rating;
        RatingCount = ratingCount;
    }
}
