using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeasonIsVisibleForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isVisibleForUser",
                table: "StorySeazons",
                newName: "IsVisibleForUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsVisibleForUser",
                table: "StorySeazons",
                newName: "isVisibleForUser");
        }
    }
}
