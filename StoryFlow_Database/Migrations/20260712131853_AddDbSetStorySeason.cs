using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSetStorySeason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StorySeason_StorySeasonId",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorySeason",
                table: "StorySeason");

            migrationBuilder.RenameTable(
                name: "StorySeason",
                newName: "StorySeazons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorySeazons",
                table: "StorySeazons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StorySeazons_StorySeasonId",
                table: "Stories",
                column: "StorySeasonId",
                principalTable: "StorySeazons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stories_StorySeazons_StorySeasonId",
                table: "Stories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorySeazons",
                table: "StorySeazons");

            migrationBuilder.RenameTable(
                name: "StorySeazons",
                newName: "StorySeason");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorySeason",
                table: "StorySeason",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stories_StorySeason_StorySeasonId",
                table: "Stories",
                column: "StorySeasonId",
                principalTable: "StorySeason",
                principalColumn: "Id");
        }
    }
}
