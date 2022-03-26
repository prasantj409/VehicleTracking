using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace VehicleTracking.API.Extensions
{
    public static class SwaggerExtension
    {
		public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(cfg =>
			{
				cfg.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Vehicle Tracking",
					Version = "v1",
					Description = "Simple RESTful API built with ASP.NET Core 5.0 to show how to create RESTful services using a decoupled, maintainable architecture."
				});

				
					cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
					{
						Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
						Name = "Authorization",
						In = ParameterLocation.Header,
						Type = SecuritySchemeType.ApiKey,
						Scheme = "Bearer"
					});

					cfg.AddSecurityRequirement(new OpenApiSecurityRequirement()
					{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,

						},
						new List<string>()

					}});
				

				//var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				//var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				//cfg.IncludeXmlComments(xmlPath, true);
				
			});
			return services;
		}

		public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger().UseSwaggerUI(options =>
			{
				//options.DefaultModelsExpandDepth(-1);
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vehicle Tracking");
				options.DocumentTitle = "Vehicle Tracking";


			});
			return app;
		}
	}
}
