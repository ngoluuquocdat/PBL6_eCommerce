using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;

namespace APIGateway
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
                     webBuilder.UseStartup<Startup>()
                       .ConfigureAppConfiguration((hostingContext, config) =>
                       {
                           config
                               .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                               .AddEnvironmentVariables();

                           config.AddOcelot("Routes", hostingContext.HostingEnvironment);
                       });
                 });
    }
}

// public static IHostBuilder CreateHostBuilder(string[] args) =>
//              Host.CreateDefaultBuilder(args)
//                 .ConfigureWebHostDefaults(webBuilder =>
//                 {
//                     webBuilder.UseStartup<Startup>();
//                 }).ConfigureAppConfiguration((HostingContext, config) => {
//                     config.SetBasePath(HostingContext.HostingEnvironment.ContentRootPath)
//                     .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
//                 });
