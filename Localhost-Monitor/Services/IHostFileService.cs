using Localhost_Monitor.Config;

namespace Localhost_Monitor.Services
{
    public interface IHostFileService
    {
        Task CheckandUpdateInHost(string filePath, IList<HostGroup> hostGroups);
    }
}
