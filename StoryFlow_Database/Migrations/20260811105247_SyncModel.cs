using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace StoryFlow_Database.Migrations
{
    /// <inheritdoc />
    public partial class SyncModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordLessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderInSeason = table.Column<int>(type: "integer", nullable: true),
                    StorySeasonId = table.Column<int>(type: "integer", nullable: true),
                    PolishTitlte = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordLessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordLessons_StorySeazons_StorySeasonId",
                        column: x => x.StorySeasonId,
                        principalTable: "StorySeazons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WordPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WordLessonId = table.Column<int>(type: "integer", nullable: false),
                    MaxPoints = table.Column<int>(type: "integer", nullable: true),
                    PointsPerWord = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordPoints_WordLessons_WordLessonId",
                        column: x => x.WordLessonId,
                        principalTable: "WordLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WordLessonId = table.Column<int>(type: "integer", nullable: false),
                    PolishWord = table.Column<string>(type: "text", nullable: true),
                    EnglishWord = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_WordLessons_WordLessonId",
                        column: x => x.WordLessonId,
                        principalTable: "WordLessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordLessons_StorySeasonId",
                table: "WordLessons",
                column: "StorySeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_WordPoints_WordLessonId",
                table: "WordPoints",
                column: "WordLessonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_WordLessonId",
                table: "Words",
                column: "WordLessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordPoints");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "WordLessons");
        }
    }
}
