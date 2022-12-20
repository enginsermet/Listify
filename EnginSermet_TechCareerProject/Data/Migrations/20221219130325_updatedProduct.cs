using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnginSermet_TechCareerProject.Data.Migrations
{
    public partial class updatedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValueSql: "(0)",
                oldClrType: typeof(decimal),
                oldType: "money",
                oldNullable: true,
                oldDefaultValueSql: "(0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Products",
                type: "money",
                nullable: true,
                defaultValueSql: "(0)",
                oldClrType: typeof(decimal),
                oldType: "money",
                oldDefaultValueSql: "(0)");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
