using System;
using System.Collections.Generic;
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

    public virtual DbSet<CinemaPrivilege> CinemaPrivileges { get; set; }

    public virtual DbSet<CinemaUser> CinemaUsers { get; set; }

    public virtual DbSet<CinemaUserRole> CinemaUserRoles { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ispp3101;Integrated Security=True;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True");
        //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ispp3101;Integrated Security=True;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True");
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
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200)
                .IsUnicode(false);

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

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket", tb => tb.HasTrigger("TrAddTicket"));

            entity.HasIndex(e => new { e.SessionId, e.Row, e.Seat }, "UQ_Ticket").IsUnique();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
