using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDescriptionTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Stories",
                newName: "PolishTitle");

            migrationBuilder.AddColumn<string>(
                name: "EnglishDescription",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishTitle",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PolishDescription",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnglishDescription",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "EnglishTitle",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "PolishDescription",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "PolishTitle",
                table: "Stories",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
