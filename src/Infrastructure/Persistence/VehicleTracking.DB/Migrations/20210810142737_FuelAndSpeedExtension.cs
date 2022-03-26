using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.DB.Migrations
{
    public partial class FuelAndSpeedExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Fuel",
                table: "tbl_device_log",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Speed",
                table: "tbl_device_log",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fuel",
                table: "tbl_device_log");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "tbl_device_log");
        }
    }
}
