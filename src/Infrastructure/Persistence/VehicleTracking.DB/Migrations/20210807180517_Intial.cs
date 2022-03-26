using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleTracking.DB.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_role",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    edited_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    edited_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_role", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "tbl_device",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    device_no = table.Column<string>(type: "varchar(50)", nullable: true),
                    device_name = table.Column<string>(type: "varchar(50)", nullable: true),
                    password = table.Column<string>(type: "varchar(50)", nullable: true),
                    role_uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    edited_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    edited_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_device", x => x.uuid);
                    table.ForeignKey(
                        name: "FK_tbl_device_tbl_role_role_uuid",
                        column: x => x.role_uuid,
                        principalTable: "tbl_role",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_name = table.Column<string>(type: "varchar(50)", nullable: true),
                    password = table.Column<string>(type: "varchar(50)", nullable: true),
                    role_uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    edited_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    edited_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.uuid);
                    table.ForeignKey(
                        name: "FK_tbl_user_tbl_role_role_uuid",
                        column: x => x.role_uuid,
                        principalTable: "tbl_role",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_device_log",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    device_uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logitute = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Latitute = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    edited_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    edited_by = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_device_log", x => x.uuid);
                    table.ForeignKey(
                        name: "FK_tbl_device_log_tbl_device_device_uuid",
                        column: x => x.device_uuid,
                        principalTable: "tbl_device",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_device_role_uuid",
                table: "tbl_device",
                column: "role_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_device_log_device_uuid",
                table: "tbl_device_log",
                column: "device_uuid");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_user_role_uuid",
                table: "tbl_user",
                column: "role_uuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_device_log");

            migrationBuilder.DropTable(
                name: "tbl_user");

            migrationBuilder.DropTable(
                name: "tbl_device");

            migrationBuilder.DropTable(
                name: "tbl_role");
        }
    }
}
