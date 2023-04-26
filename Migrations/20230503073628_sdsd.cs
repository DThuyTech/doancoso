using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login.Migrations
{
    public partial class sdsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {




            migrationBuilder.CreateTable(
                name: "diets",
                columns: table => new
                {
                    IdDiet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Buoi = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdFood = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diets", x => x.IdDiet);
                    table.ForeignKey(
                        name: "FK_diets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_diets_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_diets_FoodId",
                table: "diets",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_diets_UserId",
                table: "diets",
                column: "UserId");

         

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "detailFoodNutris");

            migrationBuilder.DropTable(
                name: "diets");

            migrationBuilder.DropTable(
                name: "foodContents");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "trains");

            migrationBuilder.DropTable(
                name: "userinfors");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "nutributions");

            migrationBuilder.DropTable(
                name: "dVTs");

            migrationBuilder.DropTable(
                name: "loaimons");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
