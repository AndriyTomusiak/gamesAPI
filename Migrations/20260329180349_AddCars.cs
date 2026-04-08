using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gamesApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Color", "Model", "Price", "Year" },
                values: new object[,]
                {
                    { 1, "Toyota", "Red", "Supra", 55000m, 2022 },
                    { 2, "Toyota", "White", "Camry", 28000m, 2023 },
                    { 3, "BMW", "Black", "M3", 72000m, 2021 },
                    { 4, "BMW", "Blue", "X5", 65000m, 2023 },
                    { 5, "Nissan", "Silver", "GTR", 115000m, 2020 },
                    { 6, "Honda", "Red", "Civic", 24000m, 2024 },
                    { 7, "Honda", "Black", "Accord", 32000m, 2023 },
                    { 8, "Mercedes", "White", "C63 AMG", 85000m, 2022 },
                    { 9, "Audi", "Gray", "RS6", 120000m, 2023 },
                    { 10, "Ford", "Yellow", "Mustang", 45000m, 2021 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
