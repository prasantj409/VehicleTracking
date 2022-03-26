﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test.DB;

namespace VehicleTracking.DB.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210810142737_FuelAndSpeedExtension")]
    partial class FuelAndSpeedExtension
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Test.Domain.Entities.Device", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("created_by");

                    b.Property<string>("DeviceName")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("device_name");

                    b.Property<string>("DeviceNo")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("device_no");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("edited_at");

                    b.Property<string>("EditedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("edited_by");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password");

                    b.Property<Guid>("RoleUUID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_uuid");

                    b.HasKey("UUID");

                    b.HasIndex("DeviceNo")
                        .IsUnique()
                        .HasFilter("[device_no] IS NOT NULL");

                    b.HasIndex("RoleUUID");

                    b.ToTable("tbl_device");
                });

            modelBuilder.Entity("Test.Domain.Entities.DeviceLog", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("created_by");

                    b.Property<Guid>("DeviceUUID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("device_uuid");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("edited_at");

                    b.Property<string>("EditedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("edited_by");

                    b.Property<double?>("Fuel")
                        .HasColumnType("float");

                    b.Property<decimal>("Latitute")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Logitute")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double?>("Speed")
                        .HasColumnType("float");

                    b.HasKey("UUID");

                    b.HasIndex("DeviceUUID");

                    b.ToTable("tbl_device_log");
                });

            modelBuilder.Entity("Test.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("edited_at");

                    b.Property<string>("EditedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("edited_by");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("UUID");

                    b.ToTable("tbl_role");

                    b.HasData(
                        new
                        {
                            UUID = new Guid("13ad315c-d2bd-4c77-a9d4-5a4a9bb2323c"),
                            CreatedAt = new DateTime(2021, 8, 7, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578),
                            CreatedBy = "migration",
                            Name = "admin"
                        },
                        new
                        {
                            UUID = new Guid("13ad318c-d2bd-4c77-a9d4-5a4a9bb2323c"),
                            CreatedAt = new DateTime(2021, 8, 7, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578),
                            CreatedBy = "migration",
                            Name = "device"
                        });
                });

            modelBuilder.Entity("Test.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("created_by");

                    b.Property<DateTime?>("EditedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("edited_at");

                    b.Property<string>("EditedBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("edited_by");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("password");

                    b.Property<Guid>("RoleUUID")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("role_uuid");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("user_name");

                    b.HasKey("UUID");

                    b.HasIndex("RoleUUID");

                    b.ToTable("tbl_user");

                    b.HasData(
                        new
                        {
                            UUID = new Guid("13ad310c-d2bd-4c77-a9d4-5a4a9bb2323c"),
                            CreatedAt = new DateTime(2021, 8, 7, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578),
                            CreatedBy = "migration",
                            Password = "system",
                            RoleUUID = new Guid("13ad315c-d2bd-4c77-a9d4-5a4a9bb2323c"),
                            Username = "system"
                        },
                        new
                        {
                            UUID = new Guid("b9c2d2a8-5ae8-11eb-ae93-0242ac130002"),
                            CreatedAt = new DateTime(2021, 8, 7, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578),
                            CreatedBy = "migration",
                            Password = "user1",
                            RoleUUID = new Guid("13ad315c-d2bd-4c77-a9d4-5a4a9bb2323c"),
                            Username = "user1"
                        });
                });

            modelBuilder.Entity("Test.Domain.Entities.Device", b =>
                {
                    b.HasOne("Test.Domain.Entities.Role", "RoleMapping")
                        .WithMany()
                        .HasForeignKey("RoleUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleMapping");
                });

            modelBuilder.Entity("Test.Domain.Entities.DeviceLog", b =>
                {
                    b.HasOne("Test.Domain.Entities.Device", "DeviceMapping")
                        .WithMany()
                        .HasForeignKey("DeviceUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeviceMapping");
                });

            modelBuilder.Entity("Test.Domain.Entities.User", b =>
                {
                    b.HasOne("Test.Domain.Entities.Role", "RoleMapping")
                        .WithMany()
                        .HasForeignKey("RoleUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleMapping");
                });
#pragma warning restore 612, 618
        }
    }
}
