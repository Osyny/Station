using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Station.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultPermissionCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionCategories",
                columns: new[] { "Id", "Description", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "Charge Station", "Charge Station", 1 },
                    { 2, "User", "User", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPasword",
                value: "$2a$11$.cDD4pYlnCKnB47rclYht.G9/epVtLBObPyuz0d36iZfIPiqu5.ye");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPasword",
                value: "$2a$11$oyK2NJVrFFGP8Zp6r8FexeIj/8JrmUSgSPPhJhrf79vpY0E78eCL2");
        }
    }
}
