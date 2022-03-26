using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.DB.Migrations
{
    public partial class DeviceLogDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Speed",
                table: "tbl_device_log",
                newName: "speed");

            migrationBuilder.RenameColumn(
                name: "Fuel",
                table: "tbl_device_log",
                newName: "fuel");

            migrationBuilder.RenameColumn(
                name: "Logitute",
                table: "tbl_device_log",
                newName: "logitude");

            migrationBuilder.RenameColumn(
                name: "Latitute",
                table: "tbl_device_log",
                newName: "latitude");

            migrationBuilder.AlterColumn<decimal>(
                name: "logitude",
                table: "tbl_device_log",
                type: "decimal(11,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "latitude",
                table: "tbl_device_log",
                type: "decimal(10,8)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "speed",
                table: "tbl_device_log",
                newName: "Speed");

            migrationBuilder.RenameColumn(
                name: "fuel",
                table: "tbl_device_log",
                newName: "Fuel");

            migrationBuilder.RenameColumn(
                name: "logitude",
                table: "tbl_device_log",
                newName: "Logitute");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "tbl_device_log",
                newName: "Latitute");

            migrationBuilder.AlterColumn<decimal>(
                name: "Logitute",
                table: "tbl_device_log",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(11,8)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitute",
                table: "tbl_device_log",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,8)");
        }
    }
}
