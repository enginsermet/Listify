using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnginSermet_TechCareerProject.Data.Migrations
{
    public partial class ProductImageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Products",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Products");
        }
    }
}
