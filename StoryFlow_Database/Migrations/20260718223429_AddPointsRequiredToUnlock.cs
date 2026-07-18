using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class AddPointsRequiredToUnlock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsRequiredToUnlock",
                table: "StorySeazons",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsRequiredToUnlock",
                table: "StorySeazons");
        }
    }
}
