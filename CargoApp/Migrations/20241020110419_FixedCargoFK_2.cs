using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    /// <inheritdoc />
    public partial class FixedCargoFK_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarRequestId = table.Column<int>(type: "int", nullable: true),
                    CarResponseId = table.Column<int>(type: "int", nullable: true),
                    TrailerType = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(9,4)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_CarRequests_CarRequestId",
                        column: x => x.CarRequestId,
                        principalTable: "CarRequests",
                        principalColumn: "CargoId");
                    table.ForeignKey(
                        name: "FK_Cargoes_CargoResponses_CarResponseId",
                        column: x => x.CarResponseId,
                        principalTable: "CargoResponses",
                        principalColumn: "CargoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_CarRequestId",
                table: "Cargoes",
                column: "CarRequestId",
                unique: true,
                filter: "[CarRequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_CarResponseId",
                table: "Cargoes",
                column: "CarResponseId",
                unique: true,
                filter: "[CarResponseId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
