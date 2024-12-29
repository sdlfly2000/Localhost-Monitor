namespace Localhost_Monitor.Config
{
    public class HostConfig
    {
        public string? hostFolderPath { get; set;}

        public List<HostGroup>? hostGroups { get; set;}
    };

    public class HostGroup
    { 
        public string? hostName { get; set; }
        public string? ip { get; set; }
    }
}
