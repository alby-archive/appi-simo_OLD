using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppiSimo.Data.Migrations
{
    public partial class AddFitAndCivicAddressFK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CivicAddress_CivicAddressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Fit_FitId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CivicAddressId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FitId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CivicAddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FitId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Fit",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CivicAddress",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Fit_UserId",
                table: "Fit",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CivicAddress_UserId",
                table: "CivicAddress",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CivicAddress_Users_UserId",
                table: "CivicAddress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fit_Users_UserId",
                table: "Fit",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CivicAddress_Users_UserId",
                table: "CivicAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_Fit_Users_UserId",
                table: "Fit");

            migrationBuilder.DropIndex(
                name: "IX_Fit_UserId",
                table: "Fit");

            migrationBuilder.DropIndex(
                name: "IX_CivicAddress_UserId",
                table: "CivicAddress");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Fit");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CivicAddress");

            migrationBuilder.AddColumn<Guid>(
                name: "CivicAddressId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FitId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CivicAddressId",
                table: "Users",
                column: "CivicAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FitId",
                table: "Users",
                column: "FitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CivicAddress_CivicAddressId",
                table: "Users",
                column: "CivicAddressId",
                principalTable: "CivicAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Fit_FitId",
                table: "Users",
                column: "FitId",
                principalTable: "Fit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
