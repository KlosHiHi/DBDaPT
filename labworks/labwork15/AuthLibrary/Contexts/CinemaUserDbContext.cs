using AuthLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthLibrary.Contexts;

public partial class CinemaUserDbContext : DbContext
{
    public CinemaUserDbContext()
    {
    }

    public CinemaUserDbContext(DbContextOptions<CinemaUserDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CinemaPrivilege> CinemaPrivileges { get; set; }

    public virtual DbSet<CinemaUser> CinemaUsers { get; set; }

    public virtual DbSet<CinemaUserRole> CinemaUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp3101;User ID=ispp3101;Password=3101;Trust Server Certificate=True");
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ispp3101;Integrated Security=True;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CinemaPrivilege>(entity =>
        {
            entity.HasKey(e => e.PrivilegeId);

            entity.ToTable("CinemaPrivilege");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CinemaUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("CinemaUser");

            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsFixedLength();

            entity.HasOne(d => d.Role).WithMany(p => p.CinemaUsers)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CinemaUser_CinemaUserRole");
        });

        modelBuilder.Entity<CinemaUserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("CinemaUserRole");

            entity.Property(e => e.RoleName).HasMaxLength(20);

            entity.HasMany(d => d.Privileges).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "CinemaRolePrivilege",
                    r => r.HasOne<CinemaPrivilege>().WithMany()
                        .HasForeignKey("PrivilegeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CinemaRolePrivilege_CinemaPrivilege"),
                    l => l.HasOne<CinemaUserRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CinemaRolePrivilege_CinemaUserRole"),
                    j =>
                    {
                        j.HasKey("RoleId", "PrivilegeId");
                        j.ToTable("CinemaRolePrivilege");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
