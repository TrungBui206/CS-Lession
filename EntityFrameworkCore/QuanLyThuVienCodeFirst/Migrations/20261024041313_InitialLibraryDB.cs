using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyThuVienCodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialLibraryDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TacGias",
                columns: table => new
                {
                    MaTacGia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTacGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QuocTich = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TacGias", x => x.MaTacGia);
                });

            migrationBuilder.CreateTable(
                name: "Sachs",
                columns: table => new
                {
                    MaSach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSach = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NamXuatBan = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaTacGia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sachs", x => x.MaSach);
                    table.ForeignKey(
                        name: "FK_Sachs_TacGias_MaTacGia",
                        column: x => x.MaTacGia,
                        principalTable: "TacGias",
                        principalColumn: "MaTacGia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sachs_MaTacGia",
                table: "Sachs",
                column: "MaTacGia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sachs");

            migrationBuilder.DropTable(
                name: "TacGias");
        }
    }
}
