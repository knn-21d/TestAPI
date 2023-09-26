using Microsoft.Extensions.Caching.Memory;

namespace TestAPI.Data
{
    public class CountersCacheHelper
    {
        private readonly IMemoryCache _cache;

        public CountersCacheHelper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void ClearOnAdd()
        {
            _cache.Remove("CountersCount");
            _cache.Remove("Counters");
            _cache.Remove("CountersCountByKeyAndValuesMoreThanOne");
        }

        public void ClearOnPatch(int id)
        {
            _cache.Remove(id);
            _cache.Remove("Counters");
            _cache.Remove("CountersCountByKeyAndValuesMoreThanOne");
        }
    }
}
