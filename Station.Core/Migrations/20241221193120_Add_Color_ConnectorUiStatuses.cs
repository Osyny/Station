using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Station.Core.Migrations
{
    /// <inheritdoc />
    public partial class Add_Color_ConnectorUiStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ConnectorUiStatuses",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "ConnectorUiStatuses");
        }
    }
}
