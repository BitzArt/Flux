using Microsoft.AspNetCore.Mvc;

namespace MudBlazor.SampleApp;

internal static class MapDataEndpointsExtension
{
    public static void MapDataEndpoints(this WebApplication app)
    {
        app.MapGet("/api/authors", ()
            => Results.Ok(WebApiData.Authors));

        app.MapGet("/api/authors/{id:int}", (
            [FromRoute] int id)
            => Results.Ok(WebApiData.Authors.FirstOrDefault(x => x.Id == id)));

        app.MapGet("/api/authors/{authorId:int}/books", (
            [FromRoute] int authorId,
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 10)
            => Results.Ok(WebApiData.BooksMap[authorId].ToPage(offset, limit)));

        app.MapGet("/api/books", (
            [FromQuery] int offset = 0,
            [FromQuery] int limit = 5,
            [FromQuery] int? authorId = null,
            [FromQuery] string? search = null,
            [FromQuery] string? order = null,
            [FromQuery] bool desc = false) =>
        {
            if (limit > 5) limit = 5;

            var q = WebApiData.Books.AsQueryable();

            if (authorId.HasValue) q = q.Where(x => x.AuthorId == authorId);
            if (!string.IsNullOrWhiteSpace(search)) q = q.Where(x => x.Title!.Contains(search, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(order))
            {
                if (order.Equals("id", StringComparison.CurrentCultureIgnoreCase)) q = !desc
                    ? q.OrderBy(x => x.Id)
                    : q.OrderByDescending(x => x.Id);

                if (order.Equals("author", StringComparison.CurrentCultureIgnoreCase)) q = !desc
                    ? q.OrderBy(x => x.AuthorId)
                    : q.OrderByDescending(x => x.AuthorId);

                if (order.Equals("title", StringComparison.CurrentCultureIgnoreCase)) q = !desc
                    ? q.OrderBy(x => x.Title)
                    : q.OrderByDescending(x => x.Title);

                if (order.Equals("publish", StringComparison.CurrentCultureIgnoreCase)) q = !desc
                    ? q.OrderBy(x => x.PublishYear)
                    : q.OrderByDescending(x => x.PublishYear);
            }

            return Results.Ok(q.ToPage(offset, limit));
        });

        app.MapGet("/api/books/{bookId:int}", (int bookId)
            => Results.Ok(WebApiData.Books.FirstOrDefault(x => x.Id == bookId)));
    }
}
