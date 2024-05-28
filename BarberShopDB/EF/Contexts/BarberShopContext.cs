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

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC07B549D953");

            entity.Property(e => e.AppointmentTime).HasColumnType("datetime");
            entity.Property(e => e.SchedulingDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Appointme__UserI__398D8EEE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E325599F");

            entity.Property(e => e.Password)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
