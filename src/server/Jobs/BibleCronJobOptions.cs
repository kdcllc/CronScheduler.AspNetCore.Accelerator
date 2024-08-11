using CronScheduler.Extensions.Scheduler;

namespace CronSchedule.AspNetCore.Accelerator.Server.Jobs;

public class BibleCronJobOptions : SchedulerOptions
{
        /// <summary>
    /// The schedule id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///  Verses.
    /// </summary>
    public string Data { get; set; }

}