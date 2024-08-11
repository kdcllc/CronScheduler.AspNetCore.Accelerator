using CronSchedule.AspNetCore.Accelerator.Server.Models;
using CronSchedule.AspNetCore.Accelerator.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CronSchedule.AspNetCore.Accelerator.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CronJobController(IJobService jobService) : ControllerBase
{

    [HttpPost("create/")]
    public async Task<ActionResult> CreateAsync(CronJob cronJob, CancellationToken cancellationToken)
    {
        var result = await jobService.CreateJobAsync(cronJob, cancellationToken);
        return CreatedAtAction("create", result);
    }

    [HttpPut("pause/{id}")]
    public async Task<ActionResult> PauseAsync(int id, CancellationToken cancellationToken)
    {

        await jobService.PauseJobAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await jobService.DeleteJobAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("get-runs/{id}")]
    public async Task<ActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var job = await jobService.GetJobRunsAsync(id, cancellationToken);
        return Ok(job);
    }

    [HttpGet("check-cron/{cronPattern}")]
    public ActionResult CheckCronPattern(string cronPattern)
    {
        return Ok(jobService.CheckCronPattern(cronPattern));
    }
}
