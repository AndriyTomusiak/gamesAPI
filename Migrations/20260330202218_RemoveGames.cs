using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamesApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Config = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Genre = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IsSystem = table.Column<bool>(type: "INTEGER", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Config", "Description", "Genre", "ImageUrl", "IsSystem", "Slug" },
                values: new object[] { 1, "{\"items\":[],\"cardBack\":\"\",\"hints\":5,\"title\":\"Magic Memory\"}", "", "Logic", "", true, "magicMemory" });
        }
    }
}
