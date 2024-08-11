using CronSchedule.AspNetCore.Accelerator.Server.Models;
using CronSchedule.AspNetCore.Accelerator.Server.Services;
using CronSchedule.AspNetCore.Accelerator.Server.Repositories;
using CronScheduler.Extensions.Scheduler;
using Microsoft.Extensions.Logging;

namespace CronSchedule.AspNetCore.Accelerator.Server.Jobs;

public class BibleCronJob : IScheduledJob
{
    private readonly BibleService _bibleService;
    private readonly BibleVerseStore _bibleVerseStore;
    private readonly ILogger<BibleCronJob> _logger;

    public BibleCronJob(
        string jobName,
        BibleService bibleService,
        BibleVerseStore bibleVerseStore,
        ICronJobRepository jobRepository,
        ILogger<BibleCronJob> logger)
    {
        Name = jobName;
        _bibleService = bibleService;
        _bibleVerseStore = bibleVerseStore;
        _logger = logger;
    }

    public string Name { get; }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("BibleCronJob is working.");

        var verses = await _bibleService.GetVerseAsync("John 3:16");
        if (verses != null)
        {
            _bibleVerseStore.AddOrUpdate(verses);
        }
    }
}
