using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Test.DB;
using Test.Repository;
using Test.Services;
using TestAPI.Setting;
using VehicleTracking.API.Controllers.Config;
using VehicleTracking.API.Extensions;
using VehicleTracking.API.RabbitMQ;
using VehicleTracking.Domain.RabbitMQ;
using VehicleTracking.Service.ExternalApiProvider;

namespace TestAPI
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

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            }).ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });
            
            services.AddCustomSwagger();

            services.AddDbContext<DataContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")); });

            services.AddService();
            services.AddRepository();

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            });

            var rabbitMQOptions = Configuration
                .GetSection("RabbitMQ")
                .Get<RabbitMQOptions>();
            
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMQOptions.HostName, c =>
                    {

                        c.Username(rabbitMQOptions.UserName);
                        c.Password(rabbitMQOptions.Password);

                    });
                });
            });

            services.AddMassTransitHostedService();
            services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
            
            services.AddHttpContextAccessor();
            services.Configure<RabbitMQOptions>(Configuration.GetSection("RabbitMQ"));
            services.Configure<MapOption>(Configuration.GetSection("MapProvider"));
            services.AddHttpClient<IExternalMapApiProvider, ExternalMapApiProvider>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();                
            }

            app.UseHttpsRedirection();
            app.UseCustomSwagger();
            app.UseRouting();
            app.UseApiResponseAndExceptionWrapper();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
