using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login.Migrations
{
    public partial class huynsdhhuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Foods_DVTid",
                table: "Foods",
                column: "DVTid");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_LoaimonId",
                table: "Foods",
                column: "LoaimonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_dVTs_DVTid",
                table: "Foods",
                column: "DVTid",
                principalTable: "dVTs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_loaimons_LoaimonId",
                table: "Foods",
                column: "LoaimonId",
                principalTable: "loaimons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_dVTs_DVTid",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_loaimons_LoaimonId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_DVTid",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_LoaimonId",
                table: "Foods");
        }
    }
}
