using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login.Migrations
{
    public partial class huydb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nutribution",
                table: "trains",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NutributioniD",
                table: "trains",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_trains_NutributioniD",
                table: "trains",
                column: "NutributioniD");

            migrationBuilder.AddForeignKey(
                name: "FK_trains_nutributions_NutributioniD",
                table: "trains",
                column: "NutributioniD",
                principalTable: "nutributions",
                principalColumn: "iD");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trains_nutributions_NutributioniD",
                table: "trains");

            migrationBuilder.DropIndex(
                name: "IX_trains_NutributioniD",
                table: "trains");

            migrationBuilder.DropColumn(
                name: "Nutribution",
                table: "trains");

            migrationBuilder.DropColumn(
                name: "NutributioniD",
                table: "trains");
        }
    }
}
