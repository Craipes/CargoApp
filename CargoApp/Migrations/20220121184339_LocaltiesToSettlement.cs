using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    public partial class LocaltiesToSettlement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localities");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityRegion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settlements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_City",
                table: "Settlements",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_CityRegion",
                table: "Settlements",
                column: "CityRegion");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_District",
                table: "Settlements",
                column: "District");

            migrationBuilder.CreateIndex(
                name: "IX_Settlements_Region",
                table: "Settlements",
                column: "Region");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "Localities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityRegion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Localities_City",
                table: "Localities",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_CityRegion",
                table: "Localities",
                column: "CityRegion");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_District",
                table: "Localities",
                column: "District");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_Region",
                table: "Localities",
                column: "Region");

            migrationBuilder.CreateIndex(
                name: "IX_Localities_StreetName",
                table: "Localities",
                column: "StreetName");
        }
    }
}
