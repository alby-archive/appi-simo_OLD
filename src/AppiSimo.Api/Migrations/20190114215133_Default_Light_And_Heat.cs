using Microsoft.EntityFrameworkCore.Migrations;

namespace AppiSimo.Api.Migrations
{
    public partial class Default_Light_And_Heat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "Lights",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Default",
                table: "Heats",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Default",
                table: "Lights");

            migrationBuilder.DropColumn(
                name: "Default",
                table: "Heats");
        }
    }
}
