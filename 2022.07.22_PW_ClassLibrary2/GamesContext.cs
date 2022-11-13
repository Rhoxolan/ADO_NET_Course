using _2022._07._22_PW_ClassLibrary1;
using Microsoft.EntityFrameworkCore;

namespace _2022._07._22_PW_ClassLibrary2
{
    public class GamesContext : DbContext
    {
        public DbSet<Game> Games { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Games; Integrated Security=True;");
        }
    }
}