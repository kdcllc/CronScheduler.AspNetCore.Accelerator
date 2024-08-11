using CronSchedule.AspNetCore.Accelerator.Server.Services;
using CronScheduler.Extensions.Scheduler;

namespace CronSchedule.AspNetCore.Accelerator.Server.Jobs;

public class BibleCronJob(
    string jobName,
    BibleCronJobOptions options,
    BibleService bibleService,
    BibleVerseStore bibleVerseStore,
    IJobService jobService,
    ILogger<BibleCronJob> logger) : IScheduledJob
{
    
    public string Name { get; } = jobName;

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("BibleCronJob is working.");

            await jobService.SetJobStartAsync(options.Id, cancellationToken);

            var verses = await bibleService.GetVerseAsync(options.Data);
            if (verses != null)
            {
                bibleVerseStore.AddOrUpdate(verses);
            }

            await jobService.SetJobEndAsync(options.Id, cancellationToken);
            
            logger.LogInformation("BibleCronJob is done.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred executing the BibleCronJob.");
            await jobService.SetJobErrorAsync(options.Id, ex.Message, cancellationToken);
        }   

    }
}
