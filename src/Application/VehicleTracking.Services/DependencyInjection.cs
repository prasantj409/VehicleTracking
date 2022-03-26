using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Services.UserServices;
using VehicleTracking.Service.DeviceService;
using VehicleTracking.Service.ExternalApiProvider;
using VehicleTracking.Service.VehicleTrackingService;

namespace Test.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IVehicleTrackingService, VehicleTrackingService>();
            services.AddScoped<IExternalMapApiProvider, ExternalMapApiProvider>();           

            return services;
        }
    }
}
