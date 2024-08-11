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

    public CronJobController(ICronJobRepository repository)
    {
        _repository = repository;
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
