using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlsv_api.Migrations
{
    /// <inheritdoc />
    public partial class V0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KHOA",
                columns: table => new
                {
                    MAKHOA = table.Column<int>(type: "int", nullable: false),
                    TENKHOA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KHOA__22F41770D9E0EA6F", x => x.MAKHOA);
                });

            migrationBuilder.CreateTable(
                name: "SINHVIEN",
                columns: table => new
                {
                    MASV = table.Column<int>(type: "int", nullable: false),
                    TENSV = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NGAYSINH = table.Column<DateTime>(type: "date", nullable: false),
                    GIOITINH = table.Column<bool>(type: "bit", nullable: false),
                    MAKHOA = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SINHVIEN__60228A28860E6CE2", x => x.MASV);
                    table.ForeignKey(
                        name: "FK__SINHVIEN__MAKHOA__267ABA7A",
                        column: x => x.MAKHOA,
                        principalTable: "KHOA",
                        principalColumn: "MAKHOA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SINHVIEN_MAKHOA",
                table: "SINHVIEN",
                column: "MAKHOA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SINHVIEN");

            migrationBuilder.DropTable(
                name: "KHOA");
        }
    }
}
