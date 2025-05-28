using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStyle.Data.Migrations
{
    /// <inheritdoc />
    public partial class OgranicenjaTermina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Datum",
                table: "Termin");

            migrationBuilder.RenameColumn(
                name: "Vrijeme",
                table: "Termin",
                newName: "DatumIVrijeme");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatumIVrijeme",
                table: "Termin",
                newName: "Vrijeme");

            migrationBuilder.AddColumn<DateTime>(
                name: "Datum",
                table: "Termin",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
