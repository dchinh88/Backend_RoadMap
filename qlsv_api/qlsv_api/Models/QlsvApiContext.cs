using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace qlsv_api.Models;

public partial class QlsvApiContext : DbContext
{
    public QlsvApiContext()
    {
    }

    public QlsvApiContext(DbContextOptions<QlsvApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Khoa> Khoas { get; set; }

    public virtual DbSet<Sinhvien> Sinhviens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost;Database=QLSV_API;UID=sa;PWD=ducchinh;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Khoa>(entity =>
        {
            entity.HasKey(e => e.Makhoa).HasName("PK__KHOA__22F41770D9E0EA6F");

            entity.ToTable("KHOA");

            entity.Property(e => e.Makhoa)
                .ValueGeneratedNever()
                .HasColumnName("MAKHOA");
            entity.Property(e => e.Tenkhoa)
                .HasMaxLength(50)
                .HasColumnName("TENKHOA");
        });

        modelBuilder.Entity<Sinhvien>(entity =>
        {
            entity.HasKey(e => e.Masv).HasName("PK__SINHVIEN__60228A28860E6CE2");

            entity.ToTable("SINHVIEN");

            entity.Property(e => e.Masv)
                .ValueGeneratedNever()
                .HasColumnName("MASV");
            entity.Property(e => e.Gioitinh).HasColumnName("GIOITINH");
            entity.Property(e => e.Makhoa).HasColumnName("MAKHOA");
            entity.Property(e => e.Ngaysinh)
                .HasColumnType("date")
                .HasColumnName("NGAYSINH");
            entity.Property(e => e.Tensv)
                .HasMaxLength(50)
                .HasColumnName("TENSV");

            entity.HasOne(d => d.MakhoaNavigation).WithMany(p => p.Sinhviens)
                .HasForeignKey(d => d.Makhoa)
                .HasConstraintName("FK__SINHVIEN__MAKHOA__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
