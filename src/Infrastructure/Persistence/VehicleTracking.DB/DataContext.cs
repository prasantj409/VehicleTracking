using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Test.DB.SeedData;
using Test.Domain.Entities;

namespace Test.DB
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceLog> DevicesLog { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

		protected override void OnModelCreating(ModelBuilder builder)
		{            
            builder.ImportUser();
            builder.Entity<Device>()
            .HasIndex(u => u.DeviceNo)
            .IsUnique();

        }

    }
}
