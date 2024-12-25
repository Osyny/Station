using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Station.Core.Migrations
{
    /// <inheritdoc />
    public partial class Add_ConnectorUiStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChargeStationId",
                table: "Connectors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConnectorUiStatusId",
                table: "Connectors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ConnectorUiStatuses",
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
                    table.PrimaryKey("PK_ConnectorUiStatuses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Connectors_ChargeStationId",
                table: "Connectors",
                column: "ChargeStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Connectors_ConnectorUiStatusId",
                table: "Connectors",
                column: "ConnectorUiStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connectors_ChargeStations_ChargeStationId",
                table: "Connectors",
                column: "ChargeStationId",
                principalTable: "ChargeStations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connectors_ConnectorUiStatuses_ConnectorUiStatusId",
                table: "Connectors",
                column: "ConnectorUiStatusId",
                principalTable: "ConnectorUiStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connectors_ChargeStations_ChargeStationId",
                table: "Connectors");

            migrationBuilder.DropForeignKey(
                name: "FK_Connectors_ConnectorUiStatuses_ConnectorUiStatusId",
                table: "Connectors");

            migrationBuilder.DropTable(
                name: "ConnectorUiStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Connectors_ChargeStationId",
                table: "Connectors");

            migrationBuilder.DropIndex(
                name: "IX_Connectors_ConnectorUiStatusId",
                table: "Connectors");

            migrationBuilder.DropColumn(
                name: "ChargeStationId",
                table: "Connectors");

            migrationBuilder.DropColumn(
                name: "ConnectorUiStatusId",
                table: "Connectors");
        }
    }
}
