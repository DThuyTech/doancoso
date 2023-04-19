using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace login.Migrations
{
    public partial class newdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "detailFoodNutris",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    NutributionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "nutributions",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nutributions", x => x.iD);
                });

         
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.DropTable(
                name: "detailFoodNutris");

            migrationBuilder.DropTable(
                name: "nutributions");

         
        }
    }
}
