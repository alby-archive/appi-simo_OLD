namespace AppiSimo.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Rates_Courts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Courts_CourtId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Heat",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Light",
                table: "Events");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtId",
                table: "Events",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HeatId",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LightId",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Heats",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HeatType = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Heats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LightType = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StartHour = table.Column<DateTime>(nullable: false),
                    EndHour = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourtRate",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RateId = table.Column<Guid>(nullable: false),
                    CourtId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtRate_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourtRate_Rates_RateId",
                        column: x => x.RateId,
                        principalTable: "Rates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_HeatId",
                table: "Events",
                column: "HeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LightId",
                table: "Events",
                column: "LightId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtRate_CourtId",
                table: "CourtRate",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtRate_RateId",
                table: "CourtRate",
                column: "RateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Courts_CourtId",
                table: "Events",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Heats_HeatId",
                table: "Events",
                column: "HeatId",
                principalTable: "Heats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Lights_LightId",
                table: "Events",
                column: "LightId",
                principalTable: "Lights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Courts_CourtId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Heats_HeatId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Lights_LightId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "CourtRate");

            migrationBuilder.DropTable(
                name: "Heats");

            migrationBuilder.DropTable(
                name: "Lights");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Events_HeatId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_LightId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "HeatId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LightId",
                table: "Events");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourtId",
                table: "Events",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<bool>(
                name: "Heat",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Light",
                table: "Events",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Courts_CourtId",
                table: "Events",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
