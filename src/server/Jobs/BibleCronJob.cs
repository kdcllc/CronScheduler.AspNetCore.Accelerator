using CronSchedule.AspNetCore.Accelerator.Server.Models;
using CronSchedule.AspNetCore.Accelerator.Server.Services;
using CronSchedule.AspNetCore.Accelerator.Server.Repositories;
using CronScheduler.Extensions.Scheduler;
using Microsoft.Extensions.Logging;

namespace CronSchedule.AspNetCore.Accelerator.Server.Jobs;

public class BibleCronJob(
    string jobName,
    BibleCronJobOptions options,
    BibleService bibleService,
    BibleVerseStore bibleVerseStore,
    ICronJobRepository jobRepository,
    ILogger<BibleCronJob> logger) : IScheduledJob
{
    
    public string Name { get; } = jobName;

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("BibleCronJob is working.");

        await jobRepository.UpdateRunAsync(_options.Id);

        var verses = await bibleService.GetVerseAsync(options.Data);
        if (verses != null)
        {
            bibleVerseStore.AddOrUpdate(verses);
        }
    }
}
