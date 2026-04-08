using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamesApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDealerSeedAndCarColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Cars",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
