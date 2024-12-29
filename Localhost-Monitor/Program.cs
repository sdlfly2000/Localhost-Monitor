using Localhost_Monitor;
using Localhost_Monitor.Config;
using Localhost_Monitor.Services;
using System.Configuration;

var builder = Host.CreateDefaultBuilder(args);

builder
    .UseWindowsService()
    .ConfigureServices(
        (context, services) =>
        {
            services
            .Configure<HostConfig>(context.Configuration.GetSection("HostMonitor"))
            .AddLogging()
            .AddTransient<IHostFileMonitorService, HostFileMonitorService>()
            .AddTransient<IHostFileService, HostFileService>()
            .AddHostedService<Worker>();
        });

var host = builder.Build();
host.Run();
