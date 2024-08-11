using System.ComponentModel.DataAnnotations;

namespace CronSchedule.AspNetCore.Accelerator.Server.Models
{
    public class CronJob
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cron { get; set; }

        [Required]
        public string TimeZone { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Data { get; set; }

        public bool RunImmediately { get; set; }
        
        public bool IsPaused { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
