using System.Text.Json;
using CronSchedule.AspNetCore.Accelerator.Server.Services.Models;

namespace CronSchedule.AspNetCore.Accelerator.Server.Services;

public class BibleService
{
    private readonly HttpClient _httpClient;

    public BibleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<BibleVerse>?> GetVerseAsync(string passage)
    {
        var response = await _httpClient.GetAsync($"https://labs.bible.org/api/?passage={passage}&type=json");
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var bibleVerses = JsonSerializer.Deserialize<List<BibleVerse>>(jsonResponse);
        return bibleVerses;
    }
}
