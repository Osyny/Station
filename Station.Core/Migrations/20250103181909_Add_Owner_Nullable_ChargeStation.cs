using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Station.Core.Migrations
{
    /// <inheritdoc />
    public partial class Add_Owner_Nullable_ChargeStation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargeStations_Owners_OwnerId",
                table: "ChargeStations");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "ChargeStations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPasword",
                value: "$2a$11$TKNtJlTSBDZA8nI4i8N5I.46OlEwwlQLsZyCbcVVDevFUyyhJUEWi");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargeStations_Owners_OwnerId",
                table: "ChargeStations",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChargeStations_Owners_OwnerId",
                table: "ChargeStations");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "ChargeStations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPasword",
                value: "$2a$11$77301yY.t9QyruVZ07C.muTJa71m75Htd6E.tUCjZHGAlWljHrbNe");

            migrationBuilder.AddForeignKey(
                name: "FK_ChargeStations_Owners_OwnerId",
                table: "ChargeStations",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
