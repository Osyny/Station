using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Station.Core.Migrations
{
    /// <inheritdoc />
    public partial class Add_StationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StationStatusId",
                table: "StationEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EnumValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationStatuses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StationEvents_StationStatusId",
                table: "StationEvents",
                column: "StationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_StationEvents_StationStatuses_StationStatusId",
                table: "StationEvents",
                column: "StationStatusId",
                principalTable: "StationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StationEvents_StationStatuses_StationStatusId",
                table: "StationEvents");

            migrationBuilder.DropTable(
                name: "StationStatuses");

            migrationBuilder.DropIndex(
                name: "IX_StationEvents_StationStatusId",
                table: "StationEvents");

            migrationBuilder.DropColumn(
                name: "StationStatusId",
                table: "StationEvents");
        }
    }
}
