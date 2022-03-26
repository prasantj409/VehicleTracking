using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.DB;
using Test.Repository;
using Test.Services;
using VehicleTracking.Domain.RabbitMQ;
using VehicleTracking.Service.ExternalApiProvider;
using VehicleTracking.Services.RecordDevicePosition.Consumer;

namespace VehicleTracking.Services.RecordDevicePosition
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleTracking.Services.RecordDevicePosition", Version = "v1" });
            });

            services.AddService();
            services.AddRepository();

            services.AddDbContext<DataContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });

            var rabbitMQOptions = Configuration
            .GetSection("RabbitMQ")
            .Get<RabbitMQOptions>();

            services.AddMassTransit(config => {
                config.AddConsumer<DeviceRecordConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMQOptions.HostName, c => {

                        c.Username(rabbitMQOptions.UserName);
                        c.Password(rabbitMQOptions.Password);

                    });
                    cfg.ReceiveEndpoint("RecordDevice", c =>
                    {
                        c.ConfigureConsumer<DeviceRecordConsumer>(ctx);
                    });

                });
            });

            services.AddMassTransitHostedService();
            services.AddHttpClient<IExternalMapApiProvider, ExternalMapApiProvider>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleTracking.Services.RecordDevicePosition v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
