using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;

namespace IMS.Friends.BLL;

public static class LoadLogger
{
    public static void AddFriendsSerilog(this ConfigureHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.File(Path.Combine("Logs", "history.log"),
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 10 * 1024 * 1024,
                retainedFileCountLimit: 2,
                rollOnFileSizeLimit: true,
                shared: true,
                flushToDiskInterval: TimeSpan.FromSeconds(1))
            .WriteTo.Console()
            .CreateBootstrapLogger();
        
        host.UseSerilog();
        
        
    }
}