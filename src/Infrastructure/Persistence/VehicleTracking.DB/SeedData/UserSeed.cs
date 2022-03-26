using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Domain.Entities;

namespace Test.DB.SeedData
{
    public static class UserSeed
    {
        public static void ImportUser(this ModelBuilder modelBuilder)
        {
            Guid roleAdmin = new Guid("13ad315c-d2bd-4c77-a9d4-5a4a9bb2323c");
            Guid roleDevice = new Guid("13ad318c-d2bd-4c77-a9d4-5a4a9bb2323c");
            modelBuilder.Entity<Role>().HasData
            (
                new Role
                {
                    UUID = roleAdmin,
                    CreatedAt = new DateTime(2021, 08, 07, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578),
                    CreatedBy = "migration",
                    Name="admin"

                },
                new Role
                {
                    UUID = roleDevice,
                    CreatedAt = new DateTime(2021, 08, 07, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578),
                    CreatedBy = "migration",
                    Name="device"

                }
            );
            modelBuilder.Entity<User>().HasData
            (
                new User
                {
                    UUID = Guid.Parse("13ad310c-d2bd-4c77-a9d4-5a4a9bb2323c"),
                    CreatedAt = new DateTime(2021, 08, 07, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578),
                    CreatedBy = "migration",
                    Username = "system",
                    Password = "system",
                    RoleUUID = roleAdmin

                },
                new User
                {
                    UUID = Guid.Parse("b9c2d2a8-5ae8-11eb-ae93-0242ac130002"),
                    CreatedAt = new DateTime(2021, 08, 07, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578),
                    CreatedBy = "migration",
                    Username = "user1",
                    Password = "user1",
                    RoleUUID = roleAdmin

                }
            );
        }
	}
}
