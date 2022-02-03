using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    public partial class CarMaxMassFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MaxMass",
                table: "Cars",
                type: "decimal(8,1)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,3)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MaxMass",
                table: "Cars",
                type: "decimal(6,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,1)");
        }
    }
}
