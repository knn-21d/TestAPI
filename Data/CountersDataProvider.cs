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
            return await _context.Counters.OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<Counter?> GetCounter(int id)
        {
            return await _context.Counters.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Counter> AddCounter(int key, int value)
        {
            Counter counter = new Counter { Key = key, Value = value };
            await _context.Counters.AddAsync(counter);
            await _context.SaveChangesAsync();
            return counter;
        }

        public async Task<int> GetCountersCount()
        {
            return await _context.Counters.CountAsync();
        }

        public async Task<IEnumerable<object>> GetCountersCountByKeyAndValuesMoreThanOne()
        {
            var result = await _context.Counters
                .GroupBy(c => c.Key)
                .Select(c => new { Key = c.Key, Count = c.Count(), CountMoreThen = c.Count(p => p.Value > 1) })
                .ToListAsync();
            return result;
        }

        public async Task<Counter> ChangeKey(Counter counter, int key)
        {
            counter.Key = key;
            await _context.SaveChangesAsync();
            return counter;
        }

        public async Task<Counter> ChangeValue(Counter counter, int value)
        {
            counter.Value = value;
            await _context.SaveChangesAsync();
            return counter;
        }
    }
}
