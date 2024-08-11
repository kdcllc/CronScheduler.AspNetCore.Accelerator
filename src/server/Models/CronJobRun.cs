using System.ComponentModel.DataAnnotations;

namespace CronSchedule.AspNetCore.Accelerator.Server.Models
{
    public class CronJobRun
    {
        [Key]
        public int Id { get; set; }

        public int CronJobId { get; set; }

        public DateTimeOffset RunStartedAt { get; set; }

        public DateTimeOffset RunEndedAt { get; set; }

        public string? Ex { get; set; }
    }
}
