using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toledo.Infrastructure.Migrations
{
    public partial class Add_Color_To_Pet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Pets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Pets");
        }
    }
}
