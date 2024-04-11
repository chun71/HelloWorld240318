using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld240318.Models;

public partial class MemoManagerContext : DbContext
{
    public MemoManagerContext(DbContextOptions<MemoManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ExcelReport> ExcelReport { get; set; }

    public virtual DbSet<Todos> Todos { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<UsersDetail> UsersDetail { get; set; }

    public virtual DbSet<UsersDetailOne> UsersDetailOne { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExcelReport>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__ExcelRep__3214EC27407F09D0");

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CellPhone)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.IdentityNum)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Todos>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Todos__3214EC27B8BABB5B");

            entity.Property(e => e.InsertDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.UIDNavigation).WithMany(p => p.Todos)
                .HasForeignKey(d => d.UID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Todos__UID__398D8EEE");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__Users__3214EC2768FBA46E");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UsersDetail>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__UsersDet__3214EC27561D99EC");

            entity.Property(e => e.Account)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CellPhone)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.IdentityNum)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TelPhone).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UsersDetailOne>(entity =>
        {
            entity.HasKey(e => e.ID).HasName("PK__UsersDet__3214EC27900DECF5");

            entity.Property(e => e.Account)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.CellPhone)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.IdentityNum)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TelPhone).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
