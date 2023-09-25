using TestAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Caching.Memory;

namespace TestAPI.Data
{
    public class CountersDataProvider
    {
        private readonly ApplicationContext _context;
        private readonly IMemoryCache _cache;

        public CountersDataProvider(IMemoryCache memoryCache)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=CountersDB;Username=postgres;Password=testpass");
            _context = new(optionsBuilder.Options);
            _context.Database.EnsureCreatedAsync();
            _cache = memoryCache;
        }

        public async Task<IEnumerable<Counter>> GetCounters()
        {
            _cache.TryGetValue("Counters", out IEnumerable<Counter>? counters);
            if (counters is null)
            {
                counters = await _context.Counters.OrderBy(c => c.Id).ToListAsync();
                _cache.Set("Counters", counters);
            }
            return counters;
        }

        public async Task<Counter?> GetCounter(int id)
        {
            _cache.TryGetValue(id, out Counter? counter);
            if (counter is null)
            {
                counter = await _context.Counters.FirstOrDefaultAsync(x => x.Id == id);
                _cache.Set(id, counter);
            }
            return counter;
        }

        public async Task<Counter> AddCounter(int key, int value)
        {
            Counter counter = new Counter { Key = key, Value = value };
            await _context.Counters.AddAsync(counter);
            await _context.SaveChangesAsync();
            _cache.Remove("Counters");
            _cache.Remove("CountersCount");
            _cache.Remove("CountersCountByKeyAndValuesMoreThanOne");
            return counter;
        }

        public async Task<int> GetCountersCount()
        {
            _cache.TryGetValue("CountersCount", out int? count);
            if (count is null)
            {
                count = await _context.Counters.CountAsync();
                _cache.Set("CountersCount", count);
            }
            return (int)count;
        }

        public async Task<IEnumerable<object>> GetCountersCountByKeyAndValuesMoreThanOne()
        {
            _cache.TryGetValue("CountersCountByKeyAndValuesMoreThanOne", out IEnumerable<object>? result);
            if (result is null)
            {
                result = await _context.Counters
                    .GroupBy(c => c.Key)
                    .Select(c => new { Key = c.Key, Count = c.Count(), CountMoreThen = c.Count(p => p.Value > 1) })
                    .ToListAsync();
                _cache.Set("CountersCountByKeyAndValuesMoreThanOne", result);
            }
            return result;
        }

        public async Task<Counter> ChangeKey(Counter counter, int key)
        {
            counter.Key = key;
            await _context.SaveChangesAsync();
            _cache.Remove("Counters");
            _cache.Remove("CountersCountByKeyAndValuesMoreThanOne");
            _cache.Remove(counter.Id);
            return counter;
        }

        public async Task<Counter> ChangeValue(Counter counter, int value)
        {
            counter.Value = value;
            await _context.SaveChangesAsync();
            _cache.Remove("Counters");
            _cache.Remove("CountersCountByKeyAndValuesMoreThanOne");
            _cache.Remove(counter.Id);
            return counter;
        }
    }
}
