using Localhost_Monitor.Config;
using System.Text;

namespace Localhost_Monitor.Services
{
    public class HostFileService : IHostFileService
    {
        public async Task CheckandUpdateInHost(string filePath, IList<HostGroup> hostGroup)
        {
            var existHosts = new List<HostGroup>();
            var fileLines = await File.ReadAllLinesAsync(filePath);
            foreach (var line in fileLines)
            {
                foreach (var host in hostGroup)
                {
                    if (string.IsNullOrEmpty(host.hostName) == false && line.Contains(host.hostName))
                    {
                        existHosts.Add(host);
                        break;
                    }
                }

                if (existHosts.Count == hostGroup.Count) 
                {
                    break;
                }
            }

            var notFoundHosts = GetNotFoundHost(hostGroup, existHosts);

            if (notFoundHosts.Any()) {
                await File.AppendAllLinesAsync(filePath, notFoundHosts);
            }
        }

        private IList<string> GetNotFoundHost(IList<HostGroup> targets, IList<HostGroup> comparers)
        {
            var diff = new List<string>();
            var comparerHasSet = comparers.ToHashSet();

            if (comparers.Count == targets.Count)
            {
                return diff;
            }

            foreach (var target in targets) 
            {
                if (comparerHasSet.Add(target))
                {
                    var valuePair = new StringBuilder();
                    valuePair.Append(target.ip);
                    valuePair.Append("\t");
                    valuePair.Append(target.hostName);

                    diff.Add(valuePair.ToString());
                };
            }

            return diff;
        }
    }
}
