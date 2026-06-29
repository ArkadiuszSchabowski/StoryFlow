using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class AddStoryNumberOfQuestionsProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfQuestions",
                table: "StoryPoints");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfQuestions",
                table: "Stories",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfQuestions",
                table: "Stories");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfQuestions",
                table: "StoryPoints",
                type: "int",
                nullable: true);
        }
    }
}
