using CronSchedule.AspNetCore.Accelerator.Server.Models;

namespace CronSchedule.AspNetCore.Accelerator.Server.Repositories;

public interface ICronJobRepository
{
    Task<IEnumerable<CronJob>> GetAllAsync();
    
    Task<CronJob?> GetByIdAsync(int id);
    Task AddAsync(CronJob cronJob);
    
    Task UpdateAsync(CronJob cronJob);
    
    Task DeleteAsync(int id);
}
