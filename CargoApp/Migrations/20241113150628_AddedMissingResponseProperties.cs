using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CargoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedMissingResponseProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "CarResponses",
                newName: "Details");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "CargoResponses",
                newName: "Details");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "CarResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "CarResponses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "CarResponses",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                table: "CarResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CarResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CarRequests",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddTime",
                table: "CargoResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "CargoResponses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "CargoResponses",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                table: "CargoResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CargoResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CargoRequests",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarResponses");

            migrationBuilder.DropColumn(
                name: "AddTime",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                table: "CargoResponses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CargoResponses");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "CarResponses",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "CargoResponses",
                newName: "Comment");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CarRequests",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CargoRequests",
                type: "decimal(10,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
