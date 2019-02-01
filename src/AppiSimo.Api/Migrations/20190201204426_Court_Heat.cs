using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppiSimo.Api.Migrations
{
    public partial class Court_Heat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HeatId",
                table: "Courts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courts_HeatId",
                table: "Courts",
                column: "HeatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Heats_HeatId",
                table: "Courts",
                column: "HeatId",
                principalTable: "Heats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Heats_HeatId",
                table: "Courts");

            migrationBuilder.DropIndex(
                name: "IX_Courts_HeatId",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "HeatId",
                table: "Courts");
        }
    }
}
