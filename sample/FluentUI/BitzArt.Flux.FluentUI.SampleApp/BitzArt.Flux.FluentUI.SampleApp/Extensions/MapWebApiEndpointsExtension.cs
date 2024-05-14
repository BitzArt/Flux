using BitzArt.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BitzArt.Flux.FluentUI.SampleApp;

public static class MapWebApiEndpointsExtension
{
    private static IEnumerable<Author> _authors =
        [
            new Author { Id = 1, Name = "Dante Alighieri" },
            new Author { Id = 2, Name = "Homer" },
            new Author { Id = 3, Name = "Leo Tolstoy" },
            new Author { Id = 4, Name = "William Shakespeare" },
            new Author { Id = 5, Name = "Fyodor Dostoevsky" },
            new Author { Id = 6, Name = "Mikhail Bulgakov" },
            new Author { Id = 7, Name = "Oscar Wilde" },
            new Author { Id = 8, Name = "F. Scott Fitzgerald" },
        ];

    private static IEnumerable<Book> _books =
    [
        new Book { Id = 1, Title = "The Divine Comedy", AuthorId = 1 },
        new Book { Id = 2, Title = "The Iliad", AuthorId = 2 },
        new Book { Id = 3, Title = "The Odyssey", AuthorId = 2 },
        new Book { Id = 4, Title = "War and Peace", AuthorId = 3 },
        new Book { Id = 5, Title = "Anna Karenina", AuthorId = 3 },
        new Book { Id = 6, Title = "Hamlet", AuthorId = 4 },
        new Book { Id = 7, Title = "Macbeth", AuthorId = 4 },
        new Book { Id = 8, Title = "Crime and Punishment", AuthorId = 5 },
        new Book { Id = 9, Title = "The Brothers Karamazov", AuthorId = 5 },
        new Book { Id = 10, Title = "The Idiot" , AuthorId = 5 },
        new Book { Id = 11, Title = "Demons", AuthorId = 5 },
        new Book { Id = 12, Title = "The Master and Margarita", AuthorId = 6 },
        new Book { Id = 13, Title = "The Picture of Dorian Gray", AuthorId = 7 },
        new Book { Id = 14, Title = "The Great Gatsby", AuthorId = 8 },
    ];


    public static IEndpointRouteBuilder MapWebApiEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/authors", () =>
        {
            return Results.Ok(_authors);
        });

        builder.MapGet("/api/books", (
            [FromQuery] int? authorId,
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 10) =>
        {
            var result = _books.AsQueryable()
                .If(authorId is not null, q => q
                    .Where(x => x.AuthorId == authorId))
                .ToPage(offset, limit);
            
            return Results.Ok(result);
        });

        return builder;
    }
}
