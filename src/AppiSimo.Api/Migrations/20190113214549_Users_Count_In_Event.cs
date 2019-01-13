using Microsoft.EntityFrameworkCore.Migrations;

namespace AppiSimo.Api.Migrations
{
    public partial class Users_Count_In_Event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Users",
                table: "Events",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Users",
                table: "Events");
        }
    }
}
