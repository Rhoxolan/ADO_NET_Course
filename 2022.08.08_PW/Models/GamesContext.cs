using Microsoft.EntityFrameworkCore;

namespace _2022._08._08_PW.Models
{
    public class GamesContext : DbContext
    {
        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<Publisher> Publishers { get; set; } = null!;

        public DbSet<Genre> Genres { get; set; } = null!;

        public DbSet<Country> Сountries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Games; Integrated Security=True;");
    }
}
