namespace Localhost_Monitor.Services
{
    public interface IHostFileMonitorService
    {
        bool StartMonitor();
        void StopMonitor();
    }
}
