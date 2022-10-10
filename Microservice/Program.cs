using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.Extensions;
using Microservice.Infrastructure.Context;
using Microsoft.AspNetCore;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microservice;

namespace Microservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            
            hostBuilder.MigrateDbContext<OrderContext>((context, services) =>
            {
                
                var env = services.GetService<IWebHostEnvironment>();
                var logger = services.GetService<ILogger<OrderContextSeed>>();
                new OrderContextSeed()
                .SeedAsync(context, env, logger)
                .Wait();
                
            });
            hostBuilder.Run();
            

        }

        static IWebHost CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
            
    }
}
