using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityStoryPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxPoints",
                table: "Stories",
                newName: "StoryPointId");

            migrationBuilder.AddColumn<bool>(
                name: "HasReceivedStoryLengthBonus",
                table: "UserStories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "StoryPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoryId = table.Column<int>(type: "int", nullable: false),
                    MaxPoints = table.Column<int>(type: "int", nullable: true),
                    PointsPerAnswer = table.Column<int>(type: "int", nullable: true),
                    BonusPointsForStoryLength = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryPoints_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryPoints_StoryId",
                table: "StoryPoints",
                column: "StoryId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryPoints");

            migrationBuilder.DropColumn(
                name: "HasReceivedStoryLengthBonus",
                table: "UserStories");

            migrationBuilder.RenameColumn(
                name: "StoryPointId",
                table: "Stories",
                newName: "MaxPoints");
        }
    }
}
