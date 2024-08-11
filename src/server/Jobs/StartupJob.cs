using CronSchedule.AspNetCore.Accelerator.Server.Services;
using CronScheduler.Extensions.StartupInitializer;

namespace CronSchedule.AspNetCore.Accelerator.Server.Jobs;

public class StartupJob(
    IJobService jobService,
    ILogger<StartupJob> logger) : IStartupJob
{
    public string Name { get; } = nameof(StartupJob);

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
            logger.LogInformation("{job} started", nameof(StartupJob));
             
            logger.LogInformation("Loading jobs from database");
            var jobCount = await jobService.LoadJobsAsync(cancellationToken);
            logger.LogInformation("Total number of jobs successfully registered: {jobCount}", jobCount);
            
            logger.LogInformation("{job} ended", nameof(StartupJob));
    }
}