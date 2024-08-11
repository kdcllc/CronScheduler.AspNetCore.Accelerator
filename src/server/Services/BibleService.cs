namespace CronSchedule.AspNetCore.Accelerator.Server.Services;

public class BibleService
{
    private readonly HttpClient _httpClient;

    public BibleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetVerseAsync(string passage)
    {
        var response = await _httpClient.GetAsync($"https://labs.bible.org/api/?passage={passage}&type=json");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
