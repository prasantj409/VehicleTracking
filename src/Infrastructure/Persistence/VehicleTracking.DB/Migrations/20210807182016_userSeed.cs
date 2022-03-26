using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.DB.Migrations
{
    public partial class userSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbl_role",
                columns: new[] { "uuid", "created_at", "created_by", "edited_at", "edited_by", "name" },
                values: new object[] { new Guid("13ad315c-d2bd-4c77-a9d4-5a4a9bb2323c"), new DateTime(2021, 8, 7, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578), "migration", null, null, "admin" });

            migrationBuilder.InsertData(
                table: "tbl_role",
                columns: new[] { "uuid", "created_at", "created_by", "edited_at", "edited_by", "name" },
                values: new object[] { new Guid("13ad318c-d2bd-4c77-a9d4-5a4a9bb2323c"), new DateTime(2021, 8, 7, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578), "migration", null, null, "device" });

            migrationBuilder.InsertData(
                table: "tbl_user",
                columns: new[] { "uuid", "created_at", "created_by", "edited_at", "edited_by", "password", "role_uuid", "user_name" },
                values: new object[] { new Guid("13ad310c-d2bd-4c77-a9d4-5a4a9bb2323c"), new DateTime(2021, 8, 7, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578), "migration", null, null, "system", new Guid("13ad315c-d2bd-4c77-a9d4-5a4a9bb2323c"), "system" });

            migrationBuilder.InsertData(
                table: "tbl_user",
                columns: new[] { "uuid", "created_at", "created_by", "edited_at", "edited_by", "password", "role_uuid", "user_name" },
                values: new object[] { new Guid("b9c2d2a8-5ae8-11eb-ae93-0242ac130002"), new DateTime(2021, 8, 7, 16, 54, 37, 199, DateTimeKind.Local).AddTicks(578), "migration", null, null, "user1", new Guid("13ad315c-d2bd-4c77-a9d4-5a4a9bb2323c"), "user1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_role",
                keyColumn: "uuid",
                keyValue: new Guid("13ad318c-d2bd-4c77-a9d4-5a4a9bb2323c"));

            migrationBuilder.DeleteData(
                table: "tbl_user",
                keyColumn: "uuid",
                keyValue: new Guid("13ad310c-d2bd-4c77-a9d4-5a4a9bb2323c"));

            migrationBuilder.DeleteData(
                table: "tbl_user",
                keyColumn: "uuid",
                keyValue: new Guid("b9c2d2a8-5ae8-11eb-ae93-0242ac130002"));

            migrationBuilder.DeleteData(
                table: "tbl_role",
                keyColumn: "uuid",
                keyValue: new Guid("13ad315c-d2bd-4c77-a9d4-5a4a9bb2323c"));
        }
    }
}
