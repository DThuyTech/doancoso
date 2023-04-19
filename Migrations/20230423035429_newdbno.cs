using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login.Migrations
{
    public partial class newdbno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "detailFoodNutris",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detailFoodNutris",
                table: "detailFoodNutris",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_detailFoodNutris_FoodId",
                table: "detailFoodNutris",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_detailFoodNutris_NutributionId",
                table: "detailFoodNutris",
                column: "NutributionId");

            migrationBuilder.AddForeignKey(
                name: "FK_detailFoodNutris_Foods_FoodId",
                table: "detailFoodNutris",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_detailFoodNutris_nutributions_NutributionId",
                table: "detailFoodNutris",
                column: "NutributionId",
                principalTable: "nutributions",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detailFoodNutris_Foods_FoodId",
                table: "detailFoodNutris");

            migrationBuilder.DropForeignKey(
                name: "FK_detailFoodNutris_nutributions_NutributionId",
                table: "detailFoodNutris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detailFoodNutris",
                table: "detailFoodNutris");

            migrationBuilder.DropIndex(
                name: "IX_detailFoodNutris_FoodId",
                table: "detailFoodNutris");

            migrationBuilder.DropIndex(
                name: "IX_detailFoodNutris_NutributionId",
                table: "detailFoodNutris");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "detailFoodNutris");
        }
    }
}
