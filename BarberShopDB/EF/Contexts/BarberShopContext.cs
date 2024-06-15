using System;
using System.Collections.Generic;
using BarberShopDB.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShopDB.EF.Contexts;

public partial class BarberShopContext : DbContext
{
    public BarberShopContext()
    {
    }

    public BarberShopContext(DbContextOptions<BarberShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Haircut> Haircuts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BarberShop;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC0719CD70DA");

            entity.HasIndex(e => e.AppointmentTime, "UQ__Appointm__1E7ECE4B45E52EF7").IsUnique();

            entity.Property(e => e.AppointmentTime).HasColumnType("datetime");
            entity.Property(e => e.SchedulingDate)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Haircut).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.HaircutId)
                .HasConstraintName("FK__Appointme__Hairc__6D0D32F4");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Appointme__UserI__6C190EBB");
        });

        modelBuilder.Entity<Haircut>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Haircuts__3214EC076E0B1E93");

            entity.Property(e => e.HaircutType)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC078A0BAC52");

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456378BAF58").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Tel)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
