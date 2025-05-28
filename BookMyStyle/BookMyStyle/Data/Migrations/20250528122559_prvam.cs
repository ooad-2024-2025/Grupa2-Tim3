using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookMyStyle.Data.Migrations
{
    /// <inheritdoc />
    public partial class prvam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    korisnikId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipFrizera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    terminID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.korisnikId);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikSalon",
                columns: table => new
                {
                    korisniksalonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salonID = table.Column<int>(type: "int", nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikSalon", x => x.korisniksalonId);
                });

            migrationBuilder.CreateTable(
                name: "Obavijest",
                columns: table => new
                {
                    obavijestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    DatumIVrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    terminID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijest", x => x.obavijestID);
                });

            migrationBuilder.CreateTable(
                name: "Recenzija",
                columns: table => new
                {
                    recenzijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocjena = table.Column<int>(type: "int", nullable: false),
                    DatumObjave = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Komentar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: false),
                    salonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzija", x => x.recenzijaID);
                });

            migrationBuilder.CreateTable(
                name: "Salon",
                columns: table => new
                {
                    salonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RadnoVrijeme = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salon", x => x.salonID);
                });

            migrationBuilder.CreateTable(
                name: "Termin",
                columns: table => new
                {
                    terminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivSalona = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AdresaSalona = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NazivFrizera = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vrijeme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    salonID = table.Column<int>(type: "int", nullable: false),
                    uslugaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termin", x => x.terminID);
                });

            migrationBuilder.CreateTable(
                name: "TerminUsluga",
                columns: table => new
                {
                    terminuslugaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    terminID = table.Column<int>(type: "int", nullable: false),
                    uslugaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminUsluga", x => x.terminuslugaId);
                });

            migrationBuilder.CreateTable(
                name: "Usluga",
                columns: table => new
                {
                    uslugaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cijena = table.Column<double>(type: "float", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Popust = table.Column<double>(type: "float", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Trajanje = table.Column<int>(type: "int", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    salonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usluga", x => x.uslugaID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "KorisnikSalon");

            migrationBuilder.DropTable(
                name: "Obavijest");

            migrationBuilder.DropTable(
                name: "Recenzija");

            migrationBuilder.DropTable(
                name: "Salon");

            migrationBuilder.DropTable(
                name: "Termin");

            migrationBuilder.DropTable(
                name: "TerminUsluga");

            migrationBuilder.DropTable(
                name: "Usluga");
        }
    }
}
