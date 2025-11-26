using CinemaClassLibrary.DTOs;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaDbLibrary.Contexts;

public partial class CinemaDbContext : DbContext
{
    public CinemaDbContext()
    {
    }

    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CinemaHall> CinemaHalls { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ispp3101;Integrated Security=True;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True");
        optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3101;User ID=ispp3101;Password=3101;Trust Server Certificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<CinemaHall>(entity =>
        {
            entity.HasKey(e => e.HallId);

            entity.ToTable("CinemaHall");

            entity.HasIndex(e => new { e.CinemaName, e.HallId }, "UQ_CinemaHall").IsUnique();

            entity.Property(e => e.HallId).ValueGeneratedOnAdd();
            entity.Property(e => e.CinemaName)
                .HasMaxLength(50)
                .HasDefaultValue("Макси");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("Film", tb => tb.HasTrigger("TrDeleteFilm"));

            entity.Property(e => e.AgeLimit)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Duration).HasDefaultValue((short)90);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.ReleaseYear).HasDefaultValueSql("(datepart(year,getdate()))");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session", tb => tb.HasTrigger("TrAddSessionPrice"));

            entity.Property(e => e.Price)
                .HasDefaultValue(200m)
                .HasColumnType("decimal(4, 0)");
            entity.Property(e => e.StartDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Film).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_Film");

            entity.HasOne(d => d.Hall).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.HallId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_CinemaHall");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket", tb => tb.HasTrigger("TrAddTicket"));

            entity.HasIndex(e => new { e.SessionId, e.Row, e.Seat }, "UQ_Ticket").IsUnique();

            entity.HasOne(d => d.Session).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Session");

            entity.HasOne(d => d.Visitor).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.VisitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_Visitor");
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.ToTable("Visitor", tb =>
                {
                    tb.HasTrigger("TrSaveEmail");
                    tb.HasTrigger("TrSaveVisitor");
                });

            entity.HasIndex(e => e.Phone, "UQ_Visitor").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
