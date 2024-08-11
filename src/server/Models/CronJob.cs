using System.ComponentModel.DataAnnotations;

namespace CronSchedule.AspNetCore.Accelerator.Server.Models;

public class CronJob
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Cron { get; set; } = string.Empty;

    [Required]
    public string TimeZone { get; set; } = "UTC";

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Data { get; set; }

    public bool RunImmediately { get; set; }
    
    public bool IsPaused { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
