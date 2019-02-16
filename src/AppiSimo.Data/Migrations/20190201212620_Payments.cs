namespace AppiSimo.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Payments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "UserEvent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Events",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "UserEvent");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Events");
        }
    }
}
