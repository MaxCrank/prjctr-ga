using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GoogleAnalyticsPushApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<GoogleAnalyticsService>("ga", client =>
            {
                client.BaseAddress = new Uri(Configuration["GaUri"]);
            });

            services.AddHttpClient<GoogleAnalyticsService>("nbu", client =>
            {
                client.BaseAddress = new Uri(Configuration["NbuUri"]);
            });

            services.AddHostedService<GoogleAnalyticsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
