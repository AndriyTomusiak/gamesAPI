using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gamesApi.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyCarDealer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Dealers_DealerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_DealerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "DealerId",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "CarDealer",
                columns: table => new
                {
                    CarsId = table.Column<int>(type: "INTEGER", nullable: false),
                    DealersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDealer", x => new { x.CarsId, x.DealersId });
                    table.ForeignKey(
                        name: "FK_CarDealer_Cars_CarsId",
                        column: x => x.CarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarDealer_Dealers_DealersId",
                        column: x => x.DealersId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarDealer_DealersId",
                table: "CarDealer",
                column: "DealersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarDealer");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Dealers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DealerId",
                table: "Cars",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DealerId",
                table: "Cars",
                column: "DealerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Dealers_DealerId",
                table: "Cars",
                column: "DealerId",
                principalTable: "Dealers",
                principalColumn: "Id");
        }
    }
}
