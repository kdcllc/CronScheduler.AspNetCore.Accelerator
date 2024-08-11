using CronSchedule.AspNetCore.Accelerator.Server.Models;
using CronSchedule.AspNetCore.Accelerator.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CronSchedule.AspNetCore.Accelerator.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BibleController : ControllerBase
{
    private readonly BibleService _bibleService;

    public BibleController(BibleService bibleService)
    {
        _bibleService = bibleService;
    }

    [HttpGet("verse")]
    public async Task<ActionResult<BibleVerse>> GetBibleVerse([FromQuery] string passage)
    {
        var bibleVerse = await _bibleService.GetVerseAsync(passage);
        return Ok(bibleVerse);
    }
}
