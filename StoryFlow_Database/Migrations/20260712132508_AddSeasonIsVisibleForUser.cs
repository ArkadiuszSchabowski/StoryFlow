using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSeasonIsVisibleForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVisibleForUser",
                table: "StorySeazons",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVisibleForUser",
                table: "StorySeazons");
        }
    }
}
