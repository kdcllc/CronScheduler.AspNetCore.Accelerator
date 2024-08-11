using CronSchedule.AspNetCore.Accelerator.Server.Models;

namespace CronSchedule.AspNetCore.Accelerator.Server.Repositories;

public interface ICronJobRepository
{
    Task<IEnumerable<CronJob>> GetAllAsync();
    
    Task<CronJob?> GetByIdAsync(int id);
    Task AddAsync(CronJob cronJob);
    
    Task UpdateAsync(CronJob cronJob);
    
    Task DeleteAsync(int id);
    Task<IEnumerable<CronJobRun>> GetAllRunsAsync();

    Task<CronJobRun?> GetRunByIdAsync(int id);
    Task<IEnumerable<CronJobRun>> GetRunsByCronJobIdAsync(int cronJobId);
    
    Task AddRunAsync(CronJobRun cronJobRun);
    
    Task UpdateRunAsync(CronJobRun cronJobRun);
    
    Task DeleteRunAsync(int id);
    Task<CronJobRun> CreateRunAsync(int cronJobId);
}
