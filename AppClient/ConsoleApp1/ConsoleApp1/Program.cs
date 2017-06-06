using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Epam.Xmp.Vts.Server.Services;

namespace Epam.Xmp.Vts.Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IRepository, Repository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "vts/workflow/{id?}", new { controller = "VacationRequests", action = "Main" });
                routes.MapRoute("signin", "vts/signin", new { controller = "User", action = "SignIn" } );
                routes.MapRoute("ping", "vts/ping", new { controller = "User", action = "Ping" });
            });
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls("http://*:5000")
                .Build();

            Console.WriteLine("Starting server...");
            host.Run();
        }
    }
}
