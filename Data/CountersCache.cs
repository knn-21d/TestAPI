using TestAPI.Models;

namespace TestAPI.Data
{
    public class CountersCache
    {
        private readonly CountersDataProvider _countersDataProvider;

        public CountersCache(CountersDataProvider provider)
        {
            _countersDataProvider = provider;
        }
    }
}
