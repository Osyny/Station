using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Station.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPasword",
                value: "$2a$11$77301yY.t9QyruVZ07C.muTJa71m75Htd6E.tUCjZHGAlWljHrbNe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPasword",
                value: "$2a$11$GWIWlYNNTovxeqM1Jp7rHuOPZOgIztq/tMte3qxj.Cr3HWZwXBNhe");
        }
    }
}
