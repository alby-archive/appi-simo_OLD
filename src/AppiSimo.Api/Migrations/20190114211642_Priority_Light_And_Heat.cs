using Microsoft.EntityFrameworkCore.Migrations;

namespace AppiSimo.Api.Migrations
{
    public partial class Priority_Light_And_Heat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Lights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Heats",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Lights");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Heats");
        }
    }
}
