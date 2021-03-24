using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerRentCar.Common.Util;
using ServerRentCar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServerRentCar
{
    public class Program
    {
        static string env = "Development";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration((hostingContext, config) =>
                //{

                //    config.AddJsonFile("appsettings.json")
                //    .AddJsonFile("appsettings." + hostingContext.HostingEnvironment.EnvironmentName + ".json")
                //    .AddEnvironmentVariables(); 
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
