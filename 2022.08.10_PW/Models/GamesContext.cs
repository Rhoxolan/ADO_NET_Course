using Microsoft.EntityFrameworkCore;

namespace _2022._08._10_PW.Models
{
    public class GamesContext : DbContext
    {
        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<Publisher> Publishers { get; set; } = null!;

        public DbSet<Genre> Genres { get; set; } = null!;

        public DbSet<Country> Сountries { get; set; } = null!;

        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<Sale> Sales { get; set; } = null!;

        public DbSet<ShowTop3PublishersPerGames> ShowTop3PublishersPerGamesCollection { get; set; } = null!;

        public DbSet<ShowTopPublisherPerGames> ShowTopPublisherPerGamesCollection { get; set; } = null!;

        public DbSet<ShowTopGenresPerGames> ShowTopGenresPerGamesCollection { get; set; } = null!;

        public DbSet<ShowTop3StylesPerSales> ShowTop3StylesPerSalesCollection { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Games; Integrated Security=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShowTop3PublishersPerGames>((act) => {
                act.HasNoKey();
                act.ToView("ShowTop3PublishersPerGames");
            });
            modelBuilder.Entity<ShowTopPublisherPerGames>((act) => {
                act.HasNoKey();
                act.ToView("ShowTopPublisherPerGames");
            });
            modelBuilder.Entity<ShowTopGenresPerGames>((act) => {
                act.HasNoKey();
                act.ToView("ShowTopGenresPerGames");
            });
            modelBuilder.Entity<ShowTop3StylesPerSales>((act) => {
                act.HasNoKey();
                act.ToView("ShowTop3StylesPerSales");
            });

            modelBuilder.Entity<Sale>();
        }
    }
}
