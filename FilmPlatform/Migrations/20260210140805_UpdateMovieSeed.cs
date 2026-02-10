using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FilmPlatform.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMovieSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedAt", "Description", "Genre", "PosterUrl", "Rating", "Title", "Type", "Year" },
                values: new object[,]
                {
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Бетмен протистоїть Джокеру, який занурює Ґотем у хаос.", "Екшн, Кримінал, Драма", null, 9.0, "Темний лицар", 0, 2008 },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Переплетені історії злочинців у Лос-Анджелесі.", "Кримінал, Драма", null, 8.9000000000000004, "Кримінальне чтиво", 0, 1994 },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Життя простої людини на фоні великих історичних подій.", "Драма, Романтика", null, 8.8000000000000007, "Форрест Ґамп", 0, 1994 },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Хакер дізнається справжню природу реальності.", "Наукова фантастика, Екшн", null, 8.6999999999999993, "Матриця", 0, 1999 },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Початок подорожі знищення Єдиного Персня.", "Фентезі, Пригоди", null, 8.8000000000000007, "Володар перснів: Братство персня", 0, 2001 },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Середзем'я занурюється у війну.", "Фентезі, Пригоди", null, 8.6999999999999993, "Володар перснів: Дві вежі", 0, 2002 },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Фінальна битва за долю Середзем'я.", "Фентезі, Пригоди", null, 9.0, "Володар перснів: Повернення короля", 0, 2003 },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Зникнення хлопчика відкриває надприродні таємниці.", "Фантастика, Драма, Трилер", null, 8.6999999999999993, "Stranger Things", 1, 2016 },
                    { 14, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Мисливець на чудовиськ шукає своє місце у жорстокому світі.", "Фентезі, Драма, Пригоди", null, 8.0999999999999996, "The Witcher", 1, 2019 },
                    { 15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Сучасна адаптація пригод Шерлока Холмса.", "Детектив, Драма", null, 9.0999999999999996, "Sherlock", 1, 2010 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedAt", "Description", "Genre", "PosterUrl", "Rating", "Title", "Type", "Year" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Історія банкіра Енді Дюфрейна, засудженого до довічного ув'язнення.", "Драма", null, 9.3000000000000007, "Втеча з Шоушенка", 0, 1994 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Сага про могутню італо-американську кримінальну сім'ю.", "Кримінал, Драма", null, 9.1999999999999993, "Хрещений батько", 0, 1972 },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Вчитель хімії стає виробником метамфетаміну.", "Кримінал, Драма, Трилер", null, 9.5, "Breaking Bad", 1, 2008 },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Епічна сага про боротьбу за Залізний трон.", "Фентезі, Драма", null, 9.3000000000000007, "Гра престолів", 1, 2011 },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Команда дослідників подорожує крізь червоточину в космосі.", "Наукова фантастика, Драма", null, 8.6999999999999993, "Інтерстеллар", 0, 2014 }
                });
        }
    }
}
