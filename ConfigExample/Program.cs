using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConfigExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public class GreetingSettings
        {
            public string Text { get; set; }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, config) =>
                {
                    var environmentName = ctx.HostingEnvironment.EnvironmentName;
                    config.Sources.Clear();
                    config.AddJsonFile("appsettings.json", optional: false, true);
                    config.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
                    
                })
                .ConfigureServices((ctx, serviceCollection) =>
                {
                    serviceCollection.Configure<GreetingSettings>(ctx.Configuration.GetSection("Greeting"));
                    serviceCollection.AddRouting();
                })
                .ConfigureWebHost(webHost => webHost
                    .Configure((ctx, app) =>
                    {
                        var env = ctx.HostingEnvironment;
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }

                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/", async context =>
                            {
                                var settings = context.RequestServices.GetService<IOptions<GreetingSettings>>();
                                await context.Response.WriteAsync(settings.Value.Text);
                            });
                        });
                    })
                    .UseKestrel());


    }
}
