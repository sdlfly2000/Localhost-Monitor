using Localhost_Monitor.Config;
using Microsoft.Extensions.Options;

namespace Localhost_Monitor.Services
{
    public class HostFileMonitorService : IHostFileMonitorService
    {
        private readonly ILogger<HostFileMonitorService> _logger;
        private readonly IHostFileService _hostFileService;
        private readonly IOptions<HostConfig> _hostConfig;
        private readonly FileSystemWatcher _watcher;

        public HostFileMonitorService(ILogger<HostFileMonitorService> logger, IHostFileService hostFileService, IOptions<HostConfig> hostConfig)
        {
            _logger = logger;
            _hostFileService = hostFileService;
            _hostConfig = hostConfig;
            _watcher = new FileSystemWatcher();
        }

        public bool StartMonitor()
        {
            try
            {
                if (string.IsNullOrEmpty(_hostConfig.Value.hostFolderPath))
                {
                    return false;
                }

                _watcher.Path = _hostConfig.Value.hostFolderPath;
                _watcher.Filter = "hosts";
                _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;

                _watcher.Changed += OnHostFileChanged;

                _watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(null, ex);
                return false;
            }

            return true;
        }

        public void StopMonitor()
        {
            if (_watcher != null) 
            { 
                _watcher.Dispose();
            }
        }

        private void OnHostFileChanged(object sender, FileSystemEventArgs e) 
        {
            if (_hostConfig.Value.hostGroups != null)
            {
                var hostGroup = _hostConfig.Value.hostGroups;
                _hostFileService.CheckandUpdateInHost(e.FullPath, hostGroup).GetAwaiter().GetResult();
            }
        }
    }
}
