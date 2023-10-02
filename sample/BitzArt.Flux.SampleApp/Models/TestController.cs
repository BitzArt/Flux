using BitzArt.Flux.SampleApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitzArt.Flux.SampleApp;

internal class TestController : ControllerBase
{
    private readonly IFluxContext _flux;

    public TestController(IFluxContext flux)
    {
        _flux = flux;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksForAuthorAsync(int authorId)
    {
        var offset = 0;
        var limit = 10;
        var page = await _flux.Set<Book>().GetPageAsync(offset, limit, authorId);
        return Ok(page);
    }

    [HttpGet]
    public async Task<IActionResult> GetBookAsync()
    {
        var id = 1;
        var book = await _flux.Set<Book>().GetAsync(id);

        return Ok(book);
    }
}
