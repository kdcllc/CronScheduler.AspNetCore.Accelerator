using CronSchedule.AspNetCore.Accelerator.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CronSchedule.AspNetCore.Accelerator.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
         public string? DbPath { get; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        

        // Define your DbSets here. For example:
        // public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<CronJob> CronJobs { get; set; }
        public DbSet<CronJobRun> CronJobRuns { get; set; }
    }
}
