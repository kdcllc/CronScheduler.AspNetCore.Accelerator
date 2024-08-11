using CronSchedule.AspNetCore.Accelerator.Server.Models;

namespace CronSchedule.AspNetCore.Accelerator.Server.Repositories;

public interface ICronJobRepository
{
    Task<IEnumerable<CronJob>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<CronJob?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(CronJob cronJob, CancellationToken cancellationToken);
    
    Task UpdateAsync(CronJob cronJob, CancellationToken cancellationToken);
    
    Task DeleteAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<CronJobRun>> GetAllRunsAsync(CancellationToken cancellationToken);

    Task<CronJobRun?> GetRunByIdAsync(int id, CancellationToken cancellationToken);

    Task<IEnumerable<CronJobRun>> GetRunsByCronJobIdAsync(int cronJobId, CancellationToken cancellationToken);
    
    Task AddRunAsync(CronJobRun cronJobRun, CancellationToken cancellationToken);
    
    Task UpdateRunAsync(CronJobRun cronJobRun, CancellationToken cancellationToken);
    
    Task DeleteRunAsync(int id, CancellationToken cancellationToken);

    Task<CronJobRun?> CreateRunAsync(int cronJobId, CancellationToken cancellationToken);
    
    Task UpdateLastRunAsync(int cronJobRunId, CancellationToken cancellationToken);

    Task UpdateExAsync(int cronJobRunId, string ex, CancellationToken cancellationToken);
}
