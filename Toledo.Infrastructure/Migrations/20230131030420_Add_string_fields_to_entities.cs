using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toledo.Infrastructure.Migrations
{
    public partial class Add_string_fields_to_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vaccine",
                table: "Pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VaccinePhoto",
                table: "Pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubZone",
                table: "Locations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Vaccine",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "VaccinePhoto",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "SubZone",
                table: "Locations");
        }
    }
}
