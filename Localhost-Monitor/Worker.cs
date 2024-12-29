using Localhost_Monitor.Services;

namespace Localhost_Monitor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHostFileMonitorService _hostFileMonitorService;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IHostFileMonitorService hostFileMonitorService)
        {
            _logger = logger;
            _hostFileMonitorService = hostFileMonitorService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _hostFileMonitorService.StartMonitor();

            return Task.CompletedTask;
        }
    }
}