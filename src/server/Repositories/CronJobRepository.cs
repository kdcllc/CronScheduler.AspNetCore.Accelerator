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

    public async Task<IEnumerable<CronJob>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.CronJobs.ToListAsync(cancellationToken);
    }

    public async Task<CronJob?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.CronJobs.FindAsync(id, cancellationToken);
    }

    public async Task AddAsync(CronJob cronJob, CancellationToken cancellationToken)
    {
        await _context.CronJobs.AddAsync(cronJob, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(CronJob cronJob, CancellationToken cancellationToken)
    {
        _context.CronJobs.Update(cronJob);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var cronJob = await _context.CronJobs.FindAsync(id, cancellationToken);
        if (cronJob != null)
        {
            _context.CronJobs.Remove(cronJob);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IEnumerable<CronJobRun>> GetAllRunsAsync(CancellationToken cancellationToken)
    {
        return await _context.CronJobRuns.ToListAsync(cancellationToken);
    }

    public async Task<CronJobRun?> GetRunByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.CronJobRuns.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<CronJobRun>> GetRunsByCronJobIdAsync(int cronJobId, CancellationToken cancellationToken)
    {
        return await _context.CronJobRuns.Where(run => run.CronJobId == cronJobId).ToListAsync(cancellationToken);
    }


    public async Task AddRunAsync(CronJobRun cronJobRun, CancellationToken cancellationToken)
    {
        await _context.CronJobRuns.AddAsync(cronJobRun, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRunAsync(CronJobRun cronJobRun, CancellationToken cancellationToken)
    {
        _context.CronJobRuns.Update(cronJobRun);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<CronJobRun?> CreateRunAsync(int cronJobId, CancellationToken cancellationToken)
    {
        var found = await GetRunsByCronJobIdAsync(cronJobId, cancellationToken);

        if (!found.Any())    
        {
            var cronJobRun = new CronJobRun
            {
                CronJobId = cronJobId,
                RunStartedAt = DateTimeOffset.UtcNow,
                RunEndedAt = DateTimeOffset.UtcNow, // This can be updated later when the run actually ends
                Ex = null // Assuming no exception initially
            };

            await _context.CronJobRuns.AddAsync(cronJobRun, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return cronJobRun;
        }


        return found.FirstOrDefault();
    }

    public async Task DeleteRunAsync(int id, CancellationToken cancellationToken)
    {
        var cronJobRun = await _context.CronJobRuns.FindAsync(id, cancellationToken);
        if (cronJobRun != null)
        {
            _context.CronJobRuns.Remove(cronJobRun);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateLastRunAsync(int cronJobRunId,CancellationToken cancellationToken)
    {
        var cronJobRun = (await GetRunsByCronJobIdAsync(cronJobRunId, cancellationToken)).FirstOrDefault();
        if (cronJobRun != null)
        {   
            cronJobRun.RunEndedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UpdateExAsync(int cronJobRunId, string ex, CancellationToken cancellationToken)
    {
        var cronJobRun = (await GetRunsByCronJobIdAsync(cronJobRunId, cancellationToken)).FirstOrDefault();
        if (cronJobRun != null)
        {   
            cronJobRun.Ex = ex;
            cronJobRun.RunEndedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
