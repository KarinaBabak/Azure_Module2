using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace AdventureWorksApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitLogConfiguration();

            try
            {
                Log.Information("The application is Starting Up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application is failed to start correctly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() // Use Serilog instead of default .NET Logger
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitLogConfiguration()
        {
            Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .WriteTo.Console(
                            outputTemplate: "[{Timestamp} {SourceContext} {Level:u4}] {Message} {NewLine} {Exception}"
                        )
                        .WriteTo.File(new CompactJsonFormatter(), "./logs/app.log")
                        .CreateLogger();
        }
    }
}
