using CronSchedule.AspNetCore.Accelerator.Server.Data;
using CronSchedule.AspNetCore.Accelerator.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CronSchedule.AspNetCore.Accelerator.Server.Repositories;

public class CronJobRepository : ICronJobRepository
{
    private readonly ApplicationDbContext _context;

    public CronJobRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CronJob>> GetAllAsync()
    {
        return await _context.CronJobs.ToListAsync();
    }

    public async Task<CronJob?> GetByIdAsync(int id)
    {
        return await _context.CronJobs.FindAsync(id);
    }

    public async Task AddAsync(CronJob cronJob)
    {
        await _context.CronJobs.AddAsync(cronJob);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CronJob cronJob)
    {
        _context.CronJobs.Update(cronJob);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cronJob = await _context.CronJobs.FindAsync(id);
        if (cronJob != null)
        {
            _context.CronJobs.Remove(cronJob);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<CronJobRun>> GetAllRunsAsync()
    {
        return await _context.CronJobRuns.ToListAsync();
    }

    public async Task<CronJobRun?> GetRunByIdAsync(int id)
    {
        return await _context.CronJobRuns.FindAsync(id);
    }

    public async Task<IEnumerable<CronJobRun>> GetRunsByCronJobIdAsync(int cronJobId)
    {
        return await _context.CronJobRuns.Where(run => run.CronJobId == cronJobId).ToListAsync();
    }

    public async Task AddRunAsync(CronJobRun cronJobRun)
    {
        await _context.CronJobRuns.AddAsync(cronJobRun);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRunAsync(CronJobRun cronJobRun)
    {
        _context.CronJobRuns.Update(cronJobRun);
        await _context.SaveChangesAsync();
    }

    public async Task<CronJobRun> CreateRunAsync(int cronJobId)
    {
        var cronJobRun = new CronJobRun
        {
            CronJobId = cronJobId,
            RunStartedAt = DateTimeOffset.UtcNow,
            RunEndedAt = DateTimeOffset.UtcNow, // This can be updated later when the run actually ends
            Ex = null // Assuming no exception initially
        };

        await _context.CronJobRuns.AddAsync(cronJobRun);
        await _context.SaveChangesAsync();

        return cronJobRun;
    }

    public async Task DeleteRunAsync(int id)
    {
        var cronJobRun = await _context.CronJobRuns.FindAsync(id);
        if (cronJobRun != null)
        {
            _context.CronJobRuns.Remove(cronJobRun);
            await _context.SaveChangesAsync();
        }
    }
}
