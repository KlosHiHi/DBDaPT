using lection1106.Model;
using Microsoft.EntityFrameworkCore;

namespace lection1106.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private string _dbPath = @"C:\Temp\ispp31\DBDaPT\lections\Lections02\lection1106\app.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {_dbPath}");
        }
    }
}
