using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login.Migrations
{
    public partial class dbhwuynewhuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_trains_NutributionId",
                table: "trains",
                column: "NutributionId");

            migrationBuilder.AddForeignKey(
                name: "FK_trains_nutributions_NutributionId",
                table: "trains",
                column: "NutributionId",
                principalTable: "nutributions",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trains_nutributions_NutributionId",
                table: "trains");

            migrationBuilder.DropIndex(
                name: "IX_trains_NutributionId",
                table: "trains");
        }
    }
}
