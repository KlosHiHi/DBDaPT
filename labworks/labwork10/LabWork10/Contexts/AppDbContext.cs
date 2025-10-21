using LabWork10.Model;
using Microsoft.EntityFrameworkCore;

namespace LabWork10.Contexts
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Film> Films { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3101;User ID=ispp3101;Password=3101;Trust Server Certificate=True");
            optionsBuilder.UseSqlServer("Data Source=FLTP-5i5-8256;Initial Catalog=ispp3101;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasMany(d => d.Genres).WithMany(p => p.Films)
                    .UsingEntity<Dictionary<string, object>>(
                        "FilmGenre",
                        r => r.HasOne<Genre>().WithMany()
                            .HasForeignKey("GenreId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_FilmGenre_Genre"),
                        l => l.HasOne<Film>().WithMany()
                            .HasForeignKey("FilmId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_FilmGenre_Film"),
                        j =>
                        {
                            j.HasKey("FilmId", "GenreId");
                            j.ToTable("FilmGenre");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
