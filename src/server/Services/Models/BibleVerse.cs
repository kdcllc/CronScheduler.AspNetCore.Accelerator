using System.Text.Json.Serialization;

namespace CronSchedule.AspNetCore.Accelerator.Server.Services.Models;

public class BibleVerse
{
    [JsonPropertyName("bookname")]
    public string BookName { get; set; }

    [JsonPropertyName("chapter")]
    public string Chapter { get; set; }
    
    [JsonPropertyName("verse")]
    public string Verse { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}