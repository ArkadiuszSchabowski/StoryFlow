using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class AddUserStars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserStories_UserId",
                table: "UserStories");

            migrationBuilder.RenameColumn(
                name: "UserPoints",
                table: "Users",
                newName: "Stars");

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_UserId_StoryId",
                table: "UserStories",
                columns: new[] { "UserId", "StoryId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserStories_UserId_StoryId",
                table: "UserStories");

            migrationBuilder.RenameColumn(
                name: "Stars",
                table: "Users",
                newName: "UserPoints");

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_UserId",
                table: "UserStories",
                column: "UserId");
        }
    }
}
