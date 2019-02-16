namespace AppiSimo.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Complete_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Heats_HeatId",
                table: "Courts");

            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Lights_LightId",
                table: "Courts");

            migrationBuilder.AddColumn<Guid>(
                name: "CivicAddressId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FitId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LightId",
                table: "Courts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HeatId",
                table: "Courts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CivicAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CivicAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fit",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CardNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fit", x => x.Id);
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
                name: "IX_Users_CivicAddressId",
                table: "Users",
                column: "CivicAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FitId",
                table: "Users",
                column: "FitId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCertificate_UserId",
                table: "MedicalCertificate",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Heats_HeatId",
                table: "Courts",
                column: "HeatId",
                principalTable: "Heats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Lights_LightId",
                table: "Courts",
                column: "LightId",
                principalTable: "Lights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Heats_HeatId",
                table: "Courts");

            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Lights_LightId",
                table: "Courts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_CivicAddress_CivicAddressId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Fit_FitId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CivicAddress");

            migrationBuilder.DropTable(
                name: "Fit");

            migrationBuilder.DropTable(
                name: "MedicalCertificate");

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

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "LightId",
                table: "Courts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "HeatId",
                table: "Courts",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Heats_HeatId",
                table: "Courts",
                column: "HeatId",
                principalTable: "Heats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Lights_LightId",
                table: "Courts",
                column: "LightId",
                principalTable: "Lights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
