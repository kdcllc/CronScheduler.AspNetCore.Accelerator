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
}
