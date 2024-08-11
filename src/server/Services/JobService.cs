
using Cronos;
using CronSchedule.AspNetCore.Accelerator.Server.Jobs;
using CronSchedule.AspNetCore.Accelerator.Server.Models;
using CronSchedule.AspNetCore.Accelerator.Server.Repositories;
using CronScheduler.Extensions.Scheduler;

namespace CronSchedule.AspNetCore.Accelerator.Server.Services;

public class JobService (
    ISchedulerRegistration schedulerRegistration,
    ICronJobRepository repo,
    IServiceProvider serviceProvider,
    ILogger<JobService> logger) : IJobService
{
    public async Task<IEnumerable<CronJobRun>> GetJobRunsAsync(int cronJobId, CancellationToken cancellationToken)
    {
        return await repo.GetRunsByCronJobIdAsync(cronJobId, cancellationToken);
    }

    public async Task<int> LoadJobsAsync(CancellationToken cancellationToken)
    {
        var jobs = (await repo.GetAllAsync(cancellationToken)).Where(x => !x.IsDeleted && !x.IsPaused).ToList();
        var jobsCount = 0;

        foreach (var job in jobs)
        {
            if (!CheckCronPattern(job.Cron)){
                logger.LogError("Failed to parse cron expression {cronExpression}", job.Cron);
                continue;
            }

            try
            {
                AddOrUpdateJob(job, onStartUp: true);
                jobsCount++;
            }
            catch (Exception)
            {
                logger.LogError("Failed to load job {jobId}", job.Id);
            }
        }

        return jobsCount;
    }    

    public async Task SetJobStartAsync(int cronJobId, CancellationToken cancellationToken)
    {
        await repo.CreateRunAsync(cronJobId, cancellationToken);
    }

    public async Task SetJobEndAsync(int cronJobId, CancellationToken cancellationToken)
    {
        await repo.UpdateLastRunAsync(cronJobId, cancellationToken);
    }

    public async Task SetJobErrorAsync(int cronJobId, string error, CancellationToken cancellationToken)
    {
        try 
        {
            await repo.UpdateExAsync(cronJobId, error, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to set job error for job {jobId}", cronJobId);
        }
    }

    public async Task<CronJob?> CreateJobAsync(
        CronJob job, 
        CancellationToken cancellationToken)
    {
        var found = await repo.GetByIdAsync(job.Id, cancellationToken);

        if (found == null)
        {
            await repo.AddAsync(job, cancellationToken);
            AddOrUpdateJob(job, onStartUp: false);
        }

        return found;
    }

    public async Task PauseJobAsync(int id, CancellationToken cancellationToken)
    {
        var found = await repo.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (found !=null)
        {
            var cronJob = new CronJob
            {
                Id = found.Id,
            };
            
            RemoveJob(cronJob.Id);

            found.IsPaused = true;
            await repo.UpdateAsync(found, cancellationToken: cancellationToken);
        }
    }

    public async Task DeleteJobAsync(int id, CancellationToken cancellationToken)
    {
        var found = await repo.GetByIdAsync(id, cancellationToken: cancellationToken);
        if (found != null)
        {
            var cronJob = new CronJob
            {
                Id = found.Id,
            };
            
            RemoveJob(cronJob.Id);

            found.IsDeleted = true;
            await repo.UpdateAsync(found, cancellationToken: cancellationToken);
        }
    }

    public bool CheckCronPattern(string cronPattern)
    {
        return CronExpression.TryParse(cronPattern, out _);
    }
    
    private void AddOrUpdateJob(CronJob? schedule, bool onStartUp)
    {
        if (schedule == null)
        {
            return;
        }
        
        var jobOptions = new BibleCronJobOptions
        {
            Id = schedule.Id,
            CronSchedule = schedule.Cron,
            Data = schedule?.Data ?? string.Empty,
            CronTimeZone = schedule?.TimeZone ?? string.Empty,
            RunImmediately = !onStartUp && (schedule?.RunImmediately ?? false),
        };

        var jobName = GetJobName(schedule?.Id ?? 0);

        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<BibleCronJob>();

        var jobService = serviceProvider.GetRequiredService<IJobService>();
        var bibleService = serviceProvider.GetRequiredService<BibleService>();
        var bibleVerseStore = serviceProvider.GetRequiredService<BibleVerseStore>();
        
        var job = new BibleCronJob(
            jobName,
            jobOptions,
            bibleService,
            bibleVerseStore,
            jobService,
            logger);

        schedulerRegistration.AddOrUpdate(job, jobOptions);
    }

    private bool RemoveJob(int id)
    {
        var jobName = GetJobName(id);
        return schedulerRegistration.Remove(jobName);
    }

    private string GetJobName(int id)
    {
        return $"{nameof(BibleCronJob)}-{id}";
    }
}
