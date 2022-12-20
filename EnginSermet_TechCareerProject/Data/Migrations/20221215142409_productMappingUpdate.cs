using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnginSermet_TechCareerProject.Data.Migrations
{
    public partial class productMappingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ListDetails",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ListDetails");
        }
    }
}
