using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gamesApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDealers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "Address", "Brand", "City", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "вул. Хрещатик, 10", "Toyota", "Київ", "AutoHub Kyiv", "+380441234567" },
                    { 2, "вул. Стрийська, 45", "BMW", "Львів", "BMW Center Lviv", "+380322345678" },
                    { 3, "вул. Дерибасівська, 22", "Nissan", "Одеса", "Nissan Motors Odesa", "+380483456789" },
                    { 4, "просп. Перемоги, 88", "Honda", "Київ", "Honda Plaza", "+380444567890" },
                    { 5, "вул. Європейська, 5", "Mercedes", "Дніпро", "Mercedes Premium", "+380565678901" },
                    { 6, "вул. Сумська, 30", "Audi", "Харків", "Audi Centre", "+380576789012" },
                    { 7, "бул. Лесі Українки, 12", "Ford", "Київ", "Ford Ukraine", "+380447890123" },
                    { 8, "вул. Городоцька, 100", "Toyota", "Львів", "Toyota West", "+380328901234" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dealers");
        }
    }
}
