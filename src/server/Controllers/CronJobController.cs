using CronSchedule.AspNetCore.Accelerator.Server.Models;
using CronSchedule.AspNetCore.Accelerator.Server.Repositories;
using CronSchedule.AspNetCore.Accelerator.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CronSchedule.AspNetCore.Accelerator.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CronJobController : ControllerBase
{
    private readonly ICronJobRepository _repository;

    private readonly BibleService _bibleService;

    public CronJobController(ICronJobRepository repository, BibleService bibleService)
    {
        _bibleService = bibleService;
        _repository = repository;
    }

    [HttpGet("bibleverse")]
    public async Task<ActionResult<string>> GetBibleVerse([FromQuery] string passage)
    {
        var verse = await _bibleService.GetVerseAsync(passage);
        return Ok(verse);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CronJob>>> GetAll()
    {
        var cronJobs = await _repository.GetAllAsync();
        return Ok(cronJobs);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CronJob>> GetById(int id)
    {
        var cronJob = await _repository.GetByIdAsync(id);
        if (cronJob == null)
        {
            return NotFound();
        }
        return Ok(cronJob);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CronJob cronJob)
    {
        await _repository.AddAsync(cronJob);
        return CreatedAtAction(nameof(GetById), new { id = cronJob.Id }, cronJob);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CronJob cronJob)
    {
        if (id != cronJob.Id)
        {
            return BadRequest();
        }

        await _repository.UpdateAsync(cronJob);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
