using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

namespace CrocLinks.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    IWebHostBuilder webHost = WebHost.CreateDefaultBuilder(args);
                    string environment = webHost.GetSetting("environment");
                    string appName = Assembly.GetExecutingAssembly().GetName().Name;

                    IConfigurationRoot config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true)
                        .AddJsonFile($"appsettings.{environment}.json", true)
                        .AddCommandLine(args)
                        .Build();

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(config)
                        .CreateLogger();

                    Log.Information("Starting web host");
                    Console.Title = "CrocLinks.UI";
                    Log.Information(@"
   _____                _      _       _        
  / ____|              | |    (_)     | |       
 | |     _ __ ___   ___| |     _ _ __ | | _____ 
 | |    | '__/ _ \ / __| |    | | '_ \| |/ / __|
 | |____| | | (_) | (__| |____| | | | |   <\__ \
  \_____|_|  \___/ \___|______|_|_| |_|_|\_\___/
");

                    webBuilder.UseStartup<Startup>()
                       .UseConfiguration(config)
                       .CaptureStartupErrors(true)
                       .UseSerilog();
                });
    }
}
