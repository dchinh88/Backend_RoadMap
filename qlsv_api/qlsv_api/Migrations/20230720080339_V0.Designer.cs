﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using qlsv_api.Models;

#nullable disable

namespace qlsv_api.Migrations
{
    [DbContext(typeof(QlsvApiContext))]
    [Migration("20230720080339_V0")]
    partial class V0
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("qlsv_api.Models.Khoa", b =>
                {
                    b.Property<int>("Makhoa")
                        .HasColumnType("int")
                        .HasColumnName("MAKHOA");

                    b.Property<string>("Tenkhoa")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("TENKHOA");

                    b.HasKey("Makhoa")
                        .HasName("PK__KHOA__22F41770D9E0EA6F");

                    b.ToTable("KHOA", (string)null);
                });

            modelBuilder.Entity("qlsv_api.Models.Sinhvien", b =>
                {
                    b.Property<int>("Masv")
                        .HasColumnType("int")
                        .HasColumnName("MASV");

                    b.Property<bool>("Gioitinh")
                        .HasColumnType("bit")
                        .HasColumnName("GIOITINH");

                    b.Property<int?>("Makhoa")
                        .HasColumnType("int")
                        .HasColumnName("MAKHOA");

                    b.Property<DateTime>("Ngaysinh")
                        .HasColumnType("date")
                        .HasColumnName("NGAYSINH");

                    b.Property<string>("Tensv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("TENSV");

                    b.HasKey("Masv")
                        .HasName("PK__SINHVIEN__60228A28860E6CE2");

                    b.HasIndex("Makhoa");

                    b.ToTable("SINHVIEN", (string)null);
                });

            modelBuilder.Entity("qlsv_api.Models.Sinhvien", b =>
                {
                    b.HasOne("qlsv_api.Models.Khoa", "MakhoaNavigation")
                        .WithMany("Sinhviens")
                        .HasForeignKey("Makhoa")
                        .HasConstraintName("FK__SINHVIEN__MAKHOA__267ABA7A");

                    b.Navigation("MakhoaNavigation");
                });

            modelBuilder.Entity("qlsv_api.Models.Khoa", b =>
                {
                    b.Navigation("Sinhviens");
                });
#pragma warning restore 612, 618
        }
    }
}
