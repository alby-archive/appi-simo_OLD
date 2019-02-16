namespace AppiSimo.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RateCourtFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "UserEvent",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "Friday",
                table: "Rates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Monday",
                table: "Rates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Saturday",
                table: "Rates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sunday",
                table: "Rates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Thursday",
                table: "Rates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tuesday",
                table: "Rates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Wednesday",
                table: "Rates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "HeatDuration",
                table: "Events",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LightDuration",
                table: "Events",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "CourtRate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CourtRate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "CourtRate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CourtRate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "CourtRate",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "UserEvent");

            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "HeatDuration",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LightDuration",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "CourtRate");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CourtRate");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "CourtRate");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CourtRate");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "CourtRate");
        }
    }
}
