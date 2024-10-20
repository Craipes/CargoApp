using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedSettlements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_CarRequests_Id",
                table: "Cargo");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_CargoResponses_CargoResponseCargoId",
                table: "Cargo");

            migrationBuilder.DropTable(
                name: "Settlements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo");

            migrationBuilder.RenameTable(
                name: "Cargo",
                newName: "Cargoes");

            migrationBuilder.RenameIndex(
                name: "IX_Cargo_CargoResponseCargoId",
                table: "Cargoes",
                newName: "IX_Cargoes_CargoResponseCargoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargoes",
                table: "Cargoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_CarRequests_Id",
                table: "Cargoes",
                column: "Id",
                principalTable: "CarRequests",
                principalColumn: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_CargoResponses_CargoResponseCargoId",
                table: "Cargoes",
                column: "CargoResponseCargoId",
                principalTable: "CargoResponses",
                principalColumn: "CargoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_CarRequests_Id",
                table: "Cargoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_CargoResponses_CargoResponseCargoId",
                table: "Cargoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargoes",
                table: "Cargoes");

            migrationBuilder.RenameTable(
                name: "Cargoes",
                newName: "Cargo");

            migrationBuilder.RenameIndex(
                name: "IX_Cargoes_CargoResponseCargoId",
                table: "Cargo",
                newName: "IX_Cargo_CargoResponseCargoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargo",
                table: "Cargo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Settlements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityRegion = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    NormalizedSettlement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(450)", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_CarRequests_Id",
                table: "Cargo",
                column: "Id",
                principalTable: "CarRequests",
                principalColumn: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_CargoResponses_CargoResponseCargoId",
                table: "Cargo",
                column: "CargoResponseCargoId",
                principalTable: "CargoResponses",
                principalColumn: "CargoId");
        }
    }
}
