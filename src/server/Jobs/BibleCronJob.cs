using CronSchedule.AspNetCore.Accelerator.Server.Services;
using CronSchedule.AspNetCore.Accelerator.Server.Repositories;
using CronScheduler.Extensions.Scheduler;

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
        try
        {
            logger.LogInformation("BibleCronJob is working.");

            await jobRepository.CreateRunAsync(options.Id, cancellationToken);

            var verses = await bibleService.GetVerseAsync(options.Data);
            if (verses != null)
            {
                bibleVerseStore.AddOrUpdate(verses);
            }

            await jobRepository.UpdateLastRunAsync(options.Id, cancellationToken);
            
            logger.LogInformation("BibleCronJob is done.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred executing the BibleCronJob.");
            
        }

    }
}
