using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    public partial class MassFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "CargoMass",
                table: "CarRequests",
                type: "decimal(8,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "CargoRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "CargoRequests");

            migrationBuilder.AlterColumn<decimal>(
                name: "CargoMass",
                table: "CarRequests",
                type: "decimal(6,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,1)");
        }
    }
}
