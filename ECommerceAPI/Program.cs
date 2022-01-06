using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;

namespace ECommerceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(x =>
                {
                    x.AddConsole();
                })
                .UseSerilog((options, logging) =>
                {
                    logging.WriteTo.Console();
                    logging.WriteTo.MSSqlServer(options.Configuration.GetConnectionString("Default"),
                        new MSSqlServerSinkOptions
                        {
                            AutoCreateSqlTable = true,
                            TableName = "ApiLogs",
                            BatchPeriod = TimeSpan.FromDays(1)
                        }, restrictedToMinimumLevel: LogEventLevel.Warning);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
