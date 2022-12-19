using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toledo.Infrastructure.Migrations
{
    public partial class Add_DNIType_To_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DNIType",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DNIType",
                table: "Users");
        }
    }
}
