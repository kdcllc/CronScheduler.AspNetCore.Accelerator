using CronSchedule.AspNetCore.Accelerator.Server.Services;
using CronSchedule.AspNetCore.Accelerator.Server.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CronSchedule.AspNetCore.Accelerator.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BibleController(
    BibleService bibleService,
    BibleVerseStore store) : ControllerBase
{
    [HttpGet("verse")]
    public async Task<ActionResult<BibleVerse>> GetBibleVerse([FromQuery] string passage)
    {
        var bibleVerse = await bibleService.GetVerseAsync(passage);
        return Ok(bibleVerse);
    }

    [HttpGet("verses")]
    public ActionResult<IEnumerable<BibleVerse>> GetBibleVerses()
    {
        var bibleVerses = store.GetAllVerses();
        return Ok(bibleVerses);
    }
}
