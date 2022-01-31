using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    public partial class SettlementsInRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeparturePlace",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "DestinationPlace",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "DeparturePlace",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "DestinationPlace",
                table: "CargoRequests");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DeparturePlaceId",
                table: "CarRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationPlaceId",
                table: "CarRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeparturePlaceId",
                table: "CargoRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationPlaceId",
                table: "CargoRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarRequests_DeparturePlaceId",
                table: "CarRequests",
                column: "DeparturePlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRequests_DestinationPlaceId",
                table: "CarRequests",
                column: "DestinationPlaceId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequests_Settlements_DeparturePlaceId",
                table: "CargoRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CargoRequests_Settlements_DestinationPlaceId",
                table: "CargoRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRequests_Settlements_DeparturePlaceId",
                table: "CarRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRequests_Settlements_DestinationPlaceId",
                table: "CarRequests");

            migrationBuilder.DropIndex(
                name: "IX_CarRequests_DeparturePlaceId",
                table: "CarRequests");

            migrationBuilder.DropIndex(
                name: "IX_CarRequests_DestinationPlaceId",
                table: "CarRequests");

            migrationBuilder.DropIndex(
                name: "IX_CargoRequests_DeparturePlaceId",
                table: "CargoRequests");

            migrationBuilder.DropIndex(
                name: "IX_CargoRequests_DestinationPlaceId",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "DeparturePlaceId",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "DestinationPlaceId",
                table: "CarRequests");

            migrationBuilder.DropColumn(
                name: "DeparturePlaceId",
                table: "CargoRequests");

            migrationBuilder.DropColumn(
                name: "DestinationPlaceId",
                table: "CargoRequests");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
        }
    }
}
