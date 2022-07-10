using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductsTask.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsTask
{
    public class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static async Task Main(string[] args)
        {
            IHost build = CreateHostBuilder(args).Build();
            using (var scope = build.Services.CreateScope())
            {
                await Seeder.SeedProductsAsync(scope.ServiceProvider);
            }

            build.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
