using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parking.Data;

namespace Parking_WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var parking = Parking.Data.Parking.Instance;
            parking.LoadCars();
            var earnedPerMinute = 0.0;
            var firstTick = true;
            var timerLog = new Timer(
                e => parking.Log(ref earnedPerMinute, ref firstTick),
                null,
                TimeSpan.Zero,
                TimeSpan.FromMinutes(1));
            var timerCharge = new Timer(
                e => parking.ChargeAFee(parking, ref earnedPerMinute),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(parking.Settings.Timeout));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{controller=Parking}/{action=Info}/{id?}");
            });
        }
    }
}
