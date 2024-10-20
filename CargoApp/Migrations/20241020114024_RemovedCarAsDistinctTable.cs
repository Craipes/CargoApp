using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCarAsDistinctTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequests_Cars_CarId",
                table: "CargoRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CarResponses_Cars_CarId",
                table: "CarResponses");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_CarResponses_CarId",
                table: "CarResponses");

            migrationBuilder.DropIndex(
                name: "IX_CargoRequests_CarId",
                table: "CargoRequests");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "CarResponses",
                newName: "Car_Type");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "CargoRequests",
                newName: "Car_Type");

            migrationBuilder.AddColumn<bool>(
                name: "Car_AvailableGPS",
                table: "CarResponses",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Car_Id",
                table: "CarResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxHeight",
                table: "CarResponses",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxLength",
                table: "CarResponses",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxMass",
                table: "CarResponses",
                type: "decimal(8,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxVolume",
                table: "CarResponses",
                type: "decimal(9,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxWidth",
                table: "CarResponses",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Car_TrailerType",
                table: "CarResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Car_AvailableGPS",
                table: "CargoRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Car_Id",
                table: "CargoRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxHeight",
                table: "CargoRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxLength",
                table: "CargoRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxMass",
                table: "CargoRequests",
                type: "decimal(8,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxVolume",
                table: "CargoRequests",
                type: "decimal(9,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Car_MaxWidth",
                table: "CargoRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Car_TrailerType",
                table: "CargoRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Car_AvailableGPS",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Car_Id",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Car_MaxHeight",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Car_MaxLength",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Car_MaxMass",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Car_MaxVolume",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Car_MaxWidth",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Car_TrailerType",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Car_AvailableGPS",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "Car_Id",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "Car_MaxHeight",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "Car_MaxLength",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "Car_MaxMass",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "Car_MaxVolume",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "Car_MaxWidth",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "Car_TrailerType",
                table: "CargoRequests");

            migrationBuilder.RenameColumn(
                name: "Car_Type",
                table: "CarResponses",
                newName: "CarId");

            migrationBuilder.RenameColumn(
                name: "Car_Type",
                table: "CargoRequests",
                newName: "CarId");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AvailableGPS = table.Column<bool>(type: "bit", nullable: true),
                    MaxHeight = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MaxLength = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MaxMass = table.Column<decimal>(type: "decimal(8,1)", nullable: false),
                    MaxVolume = table.Column<decimal>(type: "decimal(9,4)", nullable: true),
                    MaxWidth = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    TrailerType = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarResponses_CarId",
                table: "CarResponses",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoRequests_CarId",
                table: "CargoRequests",
                column: "CarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DriverId",
                table: "Cars",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoRequests_Cars_CarId",
                table: "CargoRequests",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarResponses_Cars_CarId",
                table: "CarResponses",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }
    }
}
