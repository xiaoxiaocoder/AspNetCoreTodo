using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreTodo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            InitializeDataBase(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static void InitializeDataBase(IWebHost host)
        {

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                /*
                因为InitializeAsync()返回一个 Task，就必须使用 Wait() 方法以确保它在应用程序启动前完成。
                你一般是用 await 做这件事，但是因为某些技术原因，你无法在 Program 方法中使用 await。
                这是个罕见的例外 —— 所有其它地方你都应该用 await！
                */
                //await SeedData.InitializeAsync(services);
                SeedData.InitializeAsync(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = (ILogger<Program>)services.GetService(typeof(ILogger<Program>));
                    logger.LogError(ex, "Error occured seeding the DB.");
                }
            }
        }
    }
}
