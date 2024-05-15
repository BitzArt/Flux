using Microsoft.AspNetCore.Mvc;

namespace BitzArt.Flux.FluentUI.SampleApp;

public static class MapWebApiEndpointsExtension
{
    public static IEndpointRouteBuilder MapWebApiEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/authors", () =>
        {
            return Results.Ok(Data.Authors);
        });

        builder.MapGet("/api/books", (
            [FromQuery] int? authorId,
            [FromQuery] string? sort,
            [FromQuery] bool desc = false,
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 10) =>
        {
            var result = Data.Books.AsQueryable()
                .Apply(authorId, sort, desc)
                .ToPage(offset, limit);

            return Results.Ok(result);
        });

        return builder;
    }

    private static IQueryable<Book> Apply(
        this IQueryable<Book> query,
        int? authorId,
        string? sort,
        bool desc = false)
        => query
        .WhereIf(authorId is not null, x => x.AuthorId == authorId)
        .ApplySort(sort, desc);

    private static IQueryable<Book> ApplySort(this IQueryable<Book> query, string? sort, bool desc)
    {
        var orderDirection = desc ? OrderDirection.Descending : OrderDirection.Ascending;

        if (string.IsNullOrWhiteSpace(sort))
            return query.OrderBy(x => x.Id, orderDirection);

        if (sort.Equals("Title", StringComparison.OrdinalIgnoreCase))
            return query.OrderBy(x => x.Title, orderDirection);

        if (sort.Equals("AuthorId", StringComparison.OrdinalIgnoreCase))
            return query.OrderBy(x => x.AuthorId, orderDirection);

        if (sort.Equals("Rating", StringComparison.OrdinalIgnoreCase))
            return query.OrderBy(x => x.Rating!.Rating, orderDirection);

        if (sort.Equals("RatingCount", StringComparison.OrdinalIgnoreCase))
            return query.OrderBy(x => x.Rating!.RatingCount, orderDirection);

        return query.OrderBy(x => x.Id, orderDirection);
    }
}
