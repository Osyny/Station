using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Station.Core.Migrations
{
    /// <inheritdoc />
    public partial class Edit_StationEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationEvents_StationStatuses_StationStatusId",
                table: "StationEvents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStampe",
                table: "StationEvents",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StationStatusId",
                table: "StationEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NewData",
                table: "StationEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StationEvents_StationStatuses_StationStatusId",
                table: "StationEvents",
                column: "StationStatusId",
                principalTable: "StationStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationEvents_StationStatuses_StationStatusId",
                table: "StationEvents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeStampe",
                table: "StationEvents",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<int>(
                name: "StationStatusId",
                table: "StationEvents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NewData",
                table: "StationEvents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StationEvents_StationStatuses_StationStatusId",
                table: "StationEvents",
                column: "StationStatusId",
                principalTable: "StationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
