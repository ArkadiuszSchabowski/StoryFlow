using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class AddStorySeason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderInSeason",
                table: "Stories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StorySeasonId",
                table: "Stories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StorySeason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SeasonNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorySeason", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_StorySeasonId",
                table: "Stories",
                column: "StorySeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StorySeason_StorySeasonId",
                table: "Stories",
                column: "StorySeasonId",
                principalTable: "StorySeason",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StorySeason_StorySeasonId",
                table: "Stories");

            migrationBuilder.DropTable(
                name: "StorySeason");

            migrationBuilder.DropIndex(
                name: "IX_Stories_StorySeasonId",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "OrderInSeason",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "StorySeasonId",
                table: "Stories");
        }
    }
}
