using Microsoft.Extensions.Caching.Memory;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Services
{
    public class BackgroundTaskService : BackgroundService
    {
        private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(5));
        private readonly CountersDataProvider _countersDataProvider;
        private Counter? _counter;

        public BackgroundTaskService(IMemoryCache cache)
        {
            _countersDataProvider = new(cache);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _counter = await _countersDataProvider.GetCounter(1);
            if (_counter is null) return;
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                await _countersDataProvider.ChangeValue(_counter, _counter.Value + 1);
            }
        }
    }
}
