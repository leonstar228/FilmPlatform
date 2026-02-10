using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Models;

namespace MovieLibrary.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<UserMovie> UserMovies => Set<UserMovie>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // один фільм один раз може бути в біблітеці к-ча
        builder.Entity<UserMovie>()
            .HasIndex(um => new { um.UserId, um.MovieId })
            .IsUnique();

        //зв'язки
        builder.Entity<UserMovie>()
            .HasOne(um => um.User)
            .WithMany(u => u.UserMovies)
            .HasForeignKey(um => um.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserMovie>()
            .HasOne(um => um.Movie)
            .WithMany(m => m.UserMovies)
            .HasForeignKey(um => um.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Movie>().HasData(
            new Movie
            {
                Id = 6,
                Title = "Темний лицар",
                Year = 2008,
                Genre = "Екшн, Кримінал, Драма",
                Rating = 9.0,
                Type = MediaType.Movie,
                Description = "Бетмен протистоїть Джокеру, який занурює Ґотем у хаос.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 7,
                Title = "Кримінальне чтиво",
                Year = 1994,
                Genre = "Кримінал, Драма",
                Rating = 8.9,
                Type = MediaType.Movie,
                Description = "Переплетені історії злочинців у Лос-Анджелесі.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 8,
                Title = "Форрест Ґамп",
                Year = 1994,
                Genre = "Драма, Романтика",
                Rating = 8.8,
                Type = MediaType.Movie,
                Description = "Життя простої людини на фоні великих історичних подій.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 9,
                Title = "Матриця",
                Year = 1999,
                Genre = "Наукова фантастика, Екшн",
                Rating = 8.7,
                Type = MediaType.Movie,
                Description = "Хакер дізнається справжню природу реальності.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 10,
                Title = "Володар перснів: Братство персня",
                Year = 2001,
                Genre = "Фентезі, Пригоди",
                Rating = 8.8,
                Type = MediaType.Movie,
                Description = "Початок подорожі знищення Єдиного Персня.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 11,
                Title = "Володар перснів: Дві вежі",
                Year = 2002,
                Genre = "Фентезі, Пригоди",
                Rating = 8.7,
                Type = MediaType.Movie,
                Description = "Середзем'я занурюється у війну.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 12,
                Title = "Володар перснів: Повернення короля",
                Year = 2003,
                Genre = "Фентезі, Пригоди",
                Rating = 9.0,
                Type = MediaType.Movie,
                Description = "Фінальна битва за долю Середзем'я.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 13,
                Title = "Stranger Things",
                Year = 2016,
                Genre = "Фантастика, Драма, Трилер",
                Rating = 8.7,
                Type = MediaType.Series,
                Description = "Зникнення хлопчика відкриває надприродні таємниці.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 14,
                Title = "The Witcher",
                Year = 2019,
                Genre = "Фентезі, Драма, Пригоди",
                Rating = 8.1,
                Type = MediaType.Series,
                Description = "Мисливець на чудовиськ шукає своє місце у жорстокому світі.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Movie
            {
                Id = 15,
                Title = "Sherlock",
                Year = 2010,
                Genre = "Детектив, Драма",
                Rating = 9.1,
                Type = MediaType.Series,
                Description = "Сучасна адаптація пригод Шерлока Холмса.",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

    }
}