using System.Collections.Concurrent;
using CronSchedule.AspNetCore.Accelerator.Server.Services.Models;

namespace CronSchedule.AspNetCore.Accelerator.Server.Services;

public class BibleVerseStore
{
    private static readonly Lazy<BibleVerseStore> _instance = new Lazy<BibleVerseStore>(() => new BibleVerseStore());
    private readonly ConcurrentDictionary<string, BibleVerse> _bibleVerses = new();

    private BibleVerseStore() { }

    public static BibleVerseStore Instance => _instance.Value;

    public void AddOrUpdate(IEnumerable<BibleVerse> bibleVerses)
    {
        foreach (var verse in bibleVerses)
        {
            var key = $"{verse.BookName}-{verse.Chapter}-{verse.Verse}";
            _bibleVerses.AddOrUpdate(key, verse, (k, v) => verse);
        }
    }

    public IEnumerable<BibleVerse> GetAllVerses()
    {
        return _bibleVerses.Values;
    }
}
