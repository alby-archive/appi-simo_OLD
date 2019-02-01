using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppiSimo.Api.Migrations
{
    public partial class Court_Light : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LightId",
                table: "Courts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courts_LightId",
                table: "Courts",
                column: "LightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Lights_LightId",
                table: "Courts",
                column: "LightId",
                principalTable: "Lights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Lights_LightId",
                table: "Courts");

            migrationBuilder.DropIndex(
                name: "IX_Courts_LightId",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "LightId",
                table: "Courts");
        }
    }
}
