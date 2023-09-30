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
    public async Task<IActionResult> GetBookAsync()
    {
        var book = await _flux.Model<Book>().GetAsync(1);

        return Ok(book);
    }
}
