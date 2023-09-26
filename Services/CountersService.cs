using System.Web.Http;
using System.Net;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Services
{
    public class CountersService
    {
        private readonly CountersDataProvider _countersDataProvider;

        public CountersService(CountersDataProvider countersDataProvider)
        {
            _countersDataProvider = countersDataProvider;
        }

        public async Task<Counter?> GetCounter(int id)
        {
            return await _countersDataProvider.GetCounter(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public async Task<IEnumerable<Counter>> GetCounters()
        {
            return await _countersDataProvider.GetCounters();
        }

        public async Task<int> GetCountersCount()
        {
            return await _countersDataProvider.GetCountersCount();
        }

        public async Task<IEnumerable<object>> GetCountersCountByKeyAndValuesMoreThanOne()
        {
            return await _countersDataProvider.GetCountersCountByKeyAndValuesMoreThanOne();
        }

        public async Task<Counter> AddCounter(int key, int value)
        {
            return await _countersDataProvider.AddCounter(key, value);
        }

        public async Task<Counter> ChangeKey(int id, int key)
        {
            var counter = await _countersDataProvider.GetCounter(id);
            if (counter is null) throw new HttpResponseException(HttpStatusCode.NoContent);
            return await _countersDataProvider.ChangeKey(counter, key);
        }

        public async Task<Counter> ChangeValue(int id, int value)
        {
            var counter = await _countersDataProvider.GetCounter(id);
            if (counter is null) throw new HttpResponseException(HttpStatusCode.NoContent);
            return await _countersDataProvider.ChangeValue(counter, value);
        }
    }
}
