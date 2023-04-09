using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedEventImportance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.AddColumn<int>(
                name: "EventImportanceId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EventImportances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventImportances", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EventImportances",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Low" },
                    { 1, "Medium" },
                    { 2, "High" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventImportanceId",
                table: "Events",
                column: "EventImportanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventImportances_EventImportanceId",
                table: "Events",
                column: "EventImportanceId",
                principalTable: "EventImportances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventImportances_EventImportanceId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventImportances");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventImportanceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventImportanceId",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Short" },
                    { 1, "Long" }
                });
        }
    }
}
