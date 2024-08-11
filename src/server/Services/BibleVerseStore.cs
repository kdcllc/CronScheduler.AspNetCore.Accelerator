using CronSchedule.AspNetCore.Accelerator.Server.Models;

namespace CronSchedule.AspNetCore.Accelerator.Server.Services;

public class BibleVerseStore
{
    private static readonly Lazy<BibleVerseStore> _instance = new Lazy<BibleVerseStore>(() => new BibleVerseStore());
    private readonly object _lock = new object();
    private IEnumerable<BibleVerse>? _bibleVerses;

    private BibleVerseStore() { }

    public static BibleVerseStore Instance => _instance.Value;

    public IEnumerable<BibleVerse>? BibleVerses
    {
        get
        {
            lock (_lock)
            {
                return _bibleVerses;
            }
        }
        set
        {
            lock (_lock)
            {
                _bibleVerses = value;
            }
        }
    }
}
