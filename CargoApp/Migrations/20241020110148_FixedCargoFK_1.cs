using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    /// <inheritdoc />
    public partial class FixedCargoFK_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CarRequests_CargoId",
                table: "CarRequests");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CargoResponses_CargoId",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "CargoResponses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "CarRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "CargoResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CarRequests_CargoId",
                table: "CarRequests",
                column: "CargoId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CargoResponses_CargoId",
                table: "CargoResponses",
                column: "CargoId");

            migrationBuilder.CreateTable(
                name: "Cargoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CargoResponseCargoId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Height = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Mass = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    TrailerType = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(9,4)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_CarRequests_Id",
                        column: x => x.Id,
                        principalTable: "CarRequests",
                        principalColumn: "CargoId");
                    table.ForeignKey(
                        name: "FK_Cargoes_CargoResponses_CargoResponseCargoId",
                        column: x => x.CargoResponseCargoId,
                        principalTable: "CargoResponses",
                        principalColumn: "CargoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_CargoResponseCargoId",
                table: "Cargoes",
                column: "CargoResponseCargoId",
                unique: true,
                filter: "[CargoResponseCargoId] IS NOT NULL");
        }
    }
}
