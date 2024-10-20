using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    /// <inheritdoc />
    public partial class SeriousStructureChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequests_Settlements_DeparturePlaceId",
                table: "CargoRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequests_Settlements_DestinationPlaceId",
                table: "CargoRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoResponses_AspNetUsers_SenderId",
                table: "CargoResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoResponses_CargoRequests_CargoRequestId",
                table: "CargoResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRequests_Settlements_DeparturePlaceId",
                table: "CarRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRequests_Settlements_DestinationPlaceId",
                table: "CarRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CarResponses_AspNetUsers_DriverId",
                table: "CarResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CarResponses_CarRequests_CarRequestId",
                table: "CarResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReceiverId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReceiverId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_CarRequests_DeparturePlaceId",
                table: "CarRequests");

            migrationBuilder.DropIndex(
                name: "IX_CarRequests_DestinationPlaceId",
                table: "CarRequests");

            migrationBuilder.DropIndex(
                name: "IX_CargoRequests_CarId",
                table: "CargoRequests");

            migrationBuilder.DropIndex(
                name: "IX_CargoRequests_DeparturePlaceId",
                table: "CargoRequests");

            migrationBuilder.DropIndex(
                name: "IX_CargoRequests_DestinationPlaceId",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "NumberPlate",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CargoHeight",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "CargoLength",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "CargoMass",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "CargoVolume",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "CargoWidth",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "CargoHeight",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "CargoLength",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "CargoMass",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "CargoVolume",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "CargoWidth",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "DeparturePlaceId",
                table: "CargoRequests");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "CarResponses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CarResponses_DriverId",
                table: "CarResponses",
                newName: "IX_CarResponses_UserId");

            migrationBuilder.RenameColumn(
                name: "DestinationPlaceId",
                table: "CarRequests",
                newName: "PriceType");

            migrationBuilder.RenameColumn(
                name: "DeparturePlaceId",
                table: "CarRequests",
                newName: "CargoId");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "CargoResponses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CargoResponses_SenderId",
                table: "CargoResponses",
                newName: "IX_CargoResponses_UserId");

            migrationBuilder.RenameColumn(
                name: "DestinationPlaceId",
                table: "CargoRequests",
                newName: "PriceType");

            migrationBuilder.AddColumn<bool>(
                name: "AvailableGPS",
                table: "Cars",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrailerType",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "CarResponses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "CarRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeparturePlace",
                table: "CarRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DestinationPlace",
                table: "CarRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EarlyDepartureDate",
                table: "CarRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LateDepartureDate",
                table: "CarRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "NeedsGPS",
                table: "CarRequests",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CargoId",
                table: "CargoResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "CargoRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "CargoRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeparturePlace",
                table: "CargoRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DestinationPlace",
                table: "CargoRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                columns: new[] { "ReceiverId", "SenderId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CarRequests_CargoId",
                table: "CarRequests",
                column: "CargoId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CargoResponses_CargoId",
                table: "CargoResponses",
                column: "CargoId");

            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    TrailerType = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(9,4)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    CargoResponseCargoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargo_CarRequests_Id",
                        column: x => x.Id,
                        principalTable: "CarRequests",
                        principalColumn: "CargoId");
                    table.ForeignKey(
                        name: "FK_Cargo_CargoResponses_CargoResponseCargoId",
                        column: x => x.CargoResponseCargoId,
                        principalTable: "CargoResponses",
                        principalColumn: "CargoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CargoRequests_CarId",
                table: "CargoRequests",
                column: "CarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_CargoResponseCargoId",
                table: "Cargo",
                column: "CargoResponseCargoId",
                unique: true,
                filter: "[CargoResponseCargoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoResponses_AspNetUsers_UserId",
                table: "CargoResponses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CargoResponses_CargoRequests_CargoRequestId",
                table: "CargoResponses",
                column: "CargoRequestId",
                principalTable: "CargoRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarResponses_AspNetUsers_UserId",
                table: "CarResponses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarResponses_CarRequests_CarRequestId",
                table: "CarResponses",
                column: "CarRequestId",
                principalTable: "CarRequests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReceiverId",
                table: "Reviews",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoResponses_AspNetUsers_UserId",
                table: "CargoResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoResponses_CargoRequests_CargoRequestId",
                table: "CargoResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CarResponses_AspNetUsers_UserId",
                table: "CarResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_CarResponses_CarRequests_CarRequestId",
                table: "CarResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReceiverId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CarRequests_CargoId",
                table: "CarRequests");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CargoResponses_CargoId",
                table: "CargoResponses");

            migrationBuilder.DropIndex(
                name: "IX_CargoRequests_CarId",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "AvailableGPS",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TrailerType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "DeparturePlace",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "DestinationPlace",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "EarlyDepartureDate",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "LateDepartureDate",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "NeedsGPS",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "DeparturePlace",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "DestinationPlace",
                table: "CargoRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CarResponses",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_CarResponses_UserId",
                table: "CarResponses",
                newName: "IX_CarResponses_DriverId");

            migrationBuilder.RenameColumn(
                name: "PriceType",
                table: "CarRequests",
                newName: "DestinationPlaceId");

            migrationBuilder.RenameColumn(
                name: "CargoId",
                table: "CarRequests",
                newName: "DeparturePlaceId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CargoResponses",
                newName: "SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_CargoResponses_UserId",
                table: "CargoResponses",
                newName: "IX_CargoResponses_SenderId");

            migrationBuilder.RenameColumn(
                name: "PriceType",
                table: "CargoRequests",
                newName: "DestinationPlaceId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Cars",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Cars",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberPlate",
                table: "Cars",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "CarResponses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "CargoHeight",
                table: "CarRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoLength",
                table: "CarRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoMass",
                table: "CarRequests",
                type: "decimal(8,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoVolume",
                table: "CarRequests",
                type: "decimal(9,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoWidth",
                table: "CarRequests",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoHeight",
                table: "CargoResponses",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoLength",
                table: "CargoResponses",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoMass",
                table: "CargoResponses",
                type: "decimal(6,3)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoVolume",
                table: "CargoResponses",
                type: "decimal(9,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CargoWidth",
                table: "CargoResponses",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "CargoRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DeparturePlaceId",
                table: "CargoRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReceiverId",
                table: "Reviews",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRequests_DeparturePlaceId",
                table: "CarRequests",
                column: "DeparturePlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRequests_DestinationPlaceId",
                table: "CarRequests",
                column: "DestinationPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoRequests_CarId",
                table: "CargoRequests",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoRequests_DeparturePlaceId",
                table: "CargoRequests",
                column: "DeparturePlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CargoRequests_DestinationPlaceId",
                table: "CargoRequests",
                column: "DestinationPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoRequests_Settlements_DeparturePlaceId",
                table: "CargoRequests",
                column: "DeparturePlaceId",
                principalTable: "Settlements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoRequests_Settlements_DestinationPlaceId",
                table: "CargoRequests",
                column: "DestinationPlaceId",
                principalTable: "Settlements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoResponses_AspNetUsers_SenderId",
                table: "CargoResponses",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoResponses_CargoRequests_CargoRequestId",
                table: "CargoResponses",
                column: "CargoRequestId",
                principalTable: "CargoRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRequests_Settlements_DeparturePlaceId",
                table: "CarRequests",
                column: "DeparturePlaceId",
                principalTable: "Settlements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRequests_Settlements_DestinationPlaceId",
                table: "CarRequests",
                column: "DestinationPlaceId",
                principalTable: "Settlements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarResponses_AspNetUsers_DriverId",
                table: "CarResponses",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarResponses_CarRequests_CarRequestId",
                table: "CarResponses",
                column: "CarRequestId",
                principalTable: "CarRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReceiverId",
                table: "Reviews",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
