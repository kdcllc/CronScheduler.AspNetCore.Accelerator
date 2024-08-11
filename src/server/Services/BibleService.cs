using System.Text.Json;
using CronSchedule.AspNetCore.Accelerator.Server.Services.Models;

namespace CronSchedule.AspNetCore.Accelerator.Server.Services;

public class BibleService(HttpClient httpClient)
{
    public async Task<IEnumerable<BibleVerse>?> GetVerseAsync(string passage)
    {
        var response = await httpClient.GetAsync($"https://labs.bible.org/api/?passage={passage}&type=json");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var bibleVerses = JsonSerializer.Deserialize<List<BibleVerse>>(jsonResponse);
        return bibleVerses;
    }
}
