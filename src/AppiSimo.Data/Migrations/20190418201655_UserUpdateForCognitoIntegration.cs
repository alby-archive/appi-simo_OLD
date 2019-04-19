using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppiSimo.Data.Migrations
{
    public partial class UserUpdateForCognitoIntegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CivicAddress");

            migrationBuilder.DropTable(
                name: "MedicalCertificate");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Sub",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                table: "Address",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Sub",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "CivicAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivicAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CivicAddress_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCertificate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalCertificate_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CivicAddress_UserId",
                table: "CivicAddress",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCertificate_UserId",
                table: "MedicalCertificate",
                column: "UserId");
        }
    }
}
