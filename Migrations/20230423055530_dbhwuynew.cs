using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login.Migrations
{
    public partial class dbhwuynew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "NutributioniD",
                table: "trains",
                newName: "NutributionId");

            migrationBuilder.AlterColumn<int>(
                name: "NutributionId",
                table: "trains",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NutributionId",
                table: "trains",
                newName: "NutributioniD");

            migrationBuilder.AlterColumn<int>(
                name: "NutributioniD",
                table: "trains",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Nutribution",
                table: "trains",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
