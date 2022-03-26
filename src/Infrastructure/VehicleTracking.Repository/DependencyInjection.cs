using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Repository.Repositories;
using Test.Repository.RepositoryInerface;

namespace Test.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IDeviceLogRepository, DeviceLogRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            return services;
        }
    }
}
