using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.DB.Migrations
{
    public partial class DeviceNoUniqueKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tbl_device_device_no",
                table: "tbl_device",
                column: "device_no",
                unique: true,
                filter: "[device_no] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_device_device_no",
                table: "tbl_device");
        }
    }
}
