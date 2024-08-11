using CronSchedule.AspNetCore.Accelerator.Server.Models;

namespace CronSchedule.AspNetCore.Accelerator.Server.Services;

public interface IJobService
{
    Task<IEnumerable<CronJobRun>> GetJobRunsAsync(int cronJobId, CancellationToken cancellationToken);

    Task<int> LoadJobsAsync(CancellationToken cancellationToken);

    Task SetJobStartAsync(int cronJobId, CancellationToken cancellationToken);

    Task SetJobEndAsync(int cronJobId, CancellationToken cancellationToken);

    Task SetJobErrorAsync(int cronJobId, string error, CancellationToken cancellationToken);

    Task<CronJob?> CreateJobAsync(
        CronJob job, 
        CancellationToken cancellationToken);

    Task PauseJobAsync(int id, CancellationToken cancellationToken);

    Task DeleteJobAsync(int id, CancellationToken cancellationToken);

    bool CheckCronPattern(string cronPattern);
}
