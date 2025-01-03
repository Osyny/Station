using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Station.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultPermissionActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionActions",
                columns: new[] { "Id", "Description", "Name", "PermissionCategoryId", "Value" },
                values: new object[,]
                {
                    { 1, "Edit", "Edit", 1, 1 },
                    { 2, "Create", "Create", 1, 2 },
                    { 3, "View", "View", 1, 3 },
                    { 4, "Delete", "Delete", 1, 4 },
                    { 6, "Edit", "Edit", 2, 1 },
                    { 7, "Create", "Create", 2, 2 },
                    { 8, "View", "View", 2, 3 },
                    { 9, "Delete", "Delete", 2, 4 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPasword",
                value: "$2a$11$GWIWlYNNTovxeqM1Jp7rHuOPZOgIztq/tMte3qxj.Cr3HWZwXBNhe");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PermissionActions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashPasword",
                value: "$2a$11$.cDD4pYlnCKnB47rclYht.G9/epVtLBObPyuz0d36iZfIPiqu5.ye");
        }
    }
}
