using GameStoreDbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStoreDbLibrary.Contexts;

public partial class GameStoreDbContext : DbContext
{
    public GameStoreDbContext()
    {
    }

    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lw23comment> Lw23comments { get; set; }

    public virtual DbSet<Lw23game> Lw23games { get; set; }

    public virtual DbSet<Lw23post> Lw23posts { get; set; }

    public virtual DbSet<Lw23screenshot> Lw23screenshots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3101;User ID=ispp3101;Password=3101;Trust Server Certificate=True");
        //optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3101;User ID=ispp3101;Password=3101;Trust Server Certificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lw23comment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("LW23Comments");

            entity.Property(e => e.CommentId).ValueGeneratedNever();
            entity.Property(e => e.Text).HasMaxLength(500);

            entity.HasOne(d => d.Post).WithMany(p => p.Lw23comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_LW23Comments_LW23Posts1");
        });

        modelBuilder.Entity<Lw23game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__LW23Game__A304AD9BBF9CFF87");

            entity.ToTable("LW23Games");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LogoFile).HasMaxLength(120);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Lw23post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK_LW23Posts1");

            entity.ToTable("LW23Posts");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Lw23screenshot>(entity =>
        {
            entity.HasKey(e => e.ScreenshotId);

            entity.ToTable("LW23Screenshots");

            entity.Property(e => e.FileName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Game).WithMany(p => p.Lw23screenshots)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LW23Screenshots_LW23Games");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
