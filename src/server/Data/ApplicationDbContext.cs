using Microsoft.EntityFrameworkCore;

namespace CronSchedule.AspNetCore.Accelerator.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets here. For example:
        // public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
