using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCargoAsDistinctTable : Migration
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

            migrationBuilder.RenameColumn(
                name: "CargoId",
                table: "CarRequests",
                newName: "Cargo_TrailerType");

            migrationBuilder.RenameColumn(
                name: "CargoId",
                table: "CargoResponses",
                newName: "Cargo_TrailerType");

            migrationBuilder.AddColumn<string>(
                name: "Cargo_Description",
                table: "CarRequests",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Height",
                table: "CarRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Length",
                table: "CarRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Mass",
                table: "CarRequests",
                type: "decimal(8,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Volume",
                table: "CarRequests",
                type: "decimal(9,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Width",
                table: "CarRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cargo_Description",
                table: "CargoResponses",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Height",
                table: "CargoResponses",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Length",
                table: "CargoResponses",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Mass",
                table: "CargoResponses",
                type: "decimal(8,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Volume",
                table: "CargoResponses",
                type: "decimal(9,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cargo_Width",
                table: "CargoResponses",
                type: "decimal(5,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cargo_Description",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "Cargo_Height",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "Cargo_Length",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "Cargo_Mass",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "Cargo_Volume",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "Cargo_Width",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "Cargo_Description",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "Cargo_Height",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "Cargo_Length",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "Cargo_Mass",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "Cargo_Volume",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "Cargo_Width",
                table: "CargoResponses");

            migrationBuilder.RenameColumn(
                name: "Cargo_TrailerType",
                table: "CarRequests",
                newName: "CargoId");

            migrationBuilder.RenameColumn(
                name: "Cargo_TrailerType",
                table: "CargoResponses",
                newName: "CargoId");

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
    }
}
